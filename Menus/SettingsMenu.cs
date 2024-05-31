using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SpaceInvaderPlusPlus.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace SpaceInvaderPlusPlus.Menus
{
    internal class SettingsMenu
    {
        private float Cooldawn { get; set; }
        private TimeSpan LastTime { get; set; }
        private SpriteFont OptionFont { get; set; }
        private List<string> Options { get; set; }
        private Vector2 TitleOffset { get; set; }
        private Vector2 SubTitleOffset { get; set; }
        private int LeftOffset { get; set; }
        private int RightOffset { get; set; }
        private int TopOffset { get; set; }
        private int Begin { get; set; }
        private List<Vector2> OptionOffsets { get; set; }
        private List<Color> OptionColors { get; set; }
        private int CurrentSelected { get; set; }
        private int WeaponType { get; set; }
        private int Difficulity { get; set; }
        private float MusicVol {  get; set; }
        private float SaveEffectsVol { get; set; }
        private List<string> WeaponNames { get; set; }
        private List<string> DifficulityNames { get; set; }
        private TextInputHandler TextInput { get; set; }

        public SettingsMenu(GameWindow gameWindow)
        {
            Cooldawn = 0.2f;
            LastTime = TimeSpan.FromSeconds(0.0f);

            OptionFont = Holder.CONTENT.Load<SpriteFont>("font/font_options");
            Options = new List<string>() { "RETURN", "APPLY AND SAVE", "Pilot Name", "Weapon System", "Difficulity", "Music", "Effects" };
            OptionOffsets = new List<Vector2>();
            OptionColors = new List<Color>();
            for (int i = 0; i < Options.Count; i++)
            {
                OptionOffsets.Add(OptionFont.MeasureString(Options[i]) / 2);
                if (i == 0)
                {
                    OptionColors.Add(Color.White);
                    continue;
                }
                OptionColors.Add(Color.Gray);
            }

            CurrentSelected = 0;

            Holder.PLAYERNAME = Holder.SETTINGS.LastSavedPilotName;
            WeaponType = Holder.SETTINGS.LastWeaponType;
            Difficulity = Holder.SETTINGS.LastDifficulty;
            MusicVol = Holder.SETTINGS.LastMusicVolume;
            SaveEffectsVol = Holder.SETTINGS.LastEffectsVolume;

            WeaponNames = new List<string> { "A-100 \"Warpig\"", "DS-X \"Smart Diax\"", "Rg50 \"Shashlik\"" };
            DifficulityNames = new List<string> { "Stable (EASY)", "Containgus (MEDIUM)", "Colapse (HARD)", "Quasimorphosis (HARDER)" };



            LeftOffset = 300;
            RightOffset = 25;
            TopOffset = 38;
            Begin = 400;

            TextInput = new TextInputHandler("^[a-zA-Z0-9_<>-]$", gameWindow);
        }

        public void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime - LastTime >= TimeSpan.FromSeconds(Cooldawn))
                if (!TextInput.IsTextInputActive)
                {
                    if (Holder.KSTATE.IsKeyDown(Keys.W))
                    {
                        LastTime = gameTime.TotalGameTime;
                        OptionColors[CurrentSelected] = Color.Gray;
                        if (CurrentSelected == 0)
                            CurrentSelected = Options.Count - 1;
                        else
                            CurrentSelected--;
                        OptionColors[CurrentSelected] = Color.White;
                    }
                    else if (Holder.KSTATE.IsKeyDown(Keys.S))
                    {
                        LastTime = gameTime.TotalGameTime;
                        OptionColors[CurrentSelected] = Color.Gray;
                        if (CurrentSelected == Options.Count - 1)
                            CurrentSelected = 0;
                        else
                            CurrentSelected++;
                        OptionColors[CurrentSelected] = Color.White;
                    }
                }


            for (int i = 0; i < Options.Count; i++)
                if (i == CurrentSelected && Holder.KSTATE.IsKeyDown(Keys.Enter))
                    if (Holder.KSTATE != Holder.KSTATE_PREV)
                    {
                        if (CurrentSelected == 0)
                            ReturnFrom();
                        else if (CurrentSelected == 1)
                            SaveSettings();
                        else if (CurrentSelected == 2)
                        {
                            if (!TextInput.IsTextInputActive)
                                TextInput.EnableTextInput();
                            else
                                TextInput.DisableTextInput();
                        }
                        else if (CurrentSelected == 3)
                        {
                            WeaponType++;
                            if (WeaponType == WeaponNames.Count)
                                WeaponType = 0;
                        }
                        else if (CurrentSelected == 4)
                        {
                            Difficulity++;
                            if (Difficulity == DifficulityNames.Count)
                                Difficulity = 0;
                        }
                        else if (CurrentSelected == 5)
                        {
                            MusicVol += 0.1f;
                            if (MusicVol > 1.1) MusicVol = 0;
                            MediaPlayer.Volume = MusicVol;
                        }
                        else if (CurrentSelected == 6)
                        {
                            Holder.SETTINGS.LastEffectsVolume += 0.1f;
                            if (Holder.SETTINGS.LastEffectsVolume > 1.1) Holder.SETTINGS.LastEffectsVolume = 0;
                            else if (Holder.SETTINGS.LastEffectsVolume > 1.0f) Holder.SETTINGS.LastEffectsVolume = 1.0f;
                        }
                    }
        }

        public void Draw()
        {
            for (int i = 0; i < Options.Count; i++)
            {
                if (i < 2)
                {
                    Holder.SPRITE_BATCH.DrawString(OptionFont, Options[i], new Vector2(Holder.WIDTH / 2, Begin + i * TopOffset) - OptionOffsets[i], OptionColors[i]);
                }
                else if (i == 2)
                {
                    Holder.SPRITE_BATCH.DrawString(OptionFont, Options[i], new Vector2(Holder.WIDTH / 2 - LeftOffset, Begin + i * TopOffset), OptionColors[i]);
                    Holder.SPRITE_BATCH.DrawString(OptionFont, Holder.PLAYERNAME, new Vector2(Holder.WIDTH / 2 + RightOffset, Begin + i * TopOffset), OptionColors[i]);
                }
                else if (i == 3)
                {
                    Holder.SPRITE_BATCH.DrawString(OptionFont, Options[i], new Vector2(Holder.WIDTH / 2 - LeftOffset, Begin + i * TopOffset), OptionColors[i]);
                    Holder.SPRITE_BATCH.DrawString(OptionFont, WeaponNames[WeaponType], new Vector2(Holder.WIDTH / 2 + RightOffset, Begin + i * TopOffset), OptionColors[i]);
                }
                else if (i == 4)
                {
                    Holder.SPRITE_BATCH.DrawString(OptionFont, Options[i], new Vector2(Holder.WIDTH / 2 - LeftOffset, Begin + i * TopOffset), OptionColors[i]);
                    Holder.SPRITE_BATCH.DrawString(OptionFont, DifficulityNames[Difficulity], new Vector2(Holder.WIDTH / 2 + RightOffset, Begin + i * TopOffset), OptionColors[i]);
                }
                else if (i == 5)
                {
                    Holder.SPRITE_BATCH.DrawString(OptionFont, Options[i], new Vector2(Holder.WIDTH / 2 - LeftOffset, Begin + i * TopOffset), OptionColors[i]);
                    Holder.SPRITE_BATCH.DrawString(OptionFont, ((int)(MusicVol * 10)).ToString(), new Vector2(Holder.WIDTH / 2 + RightOffset, Begin + i * TopOffset), OptionColors[i]);
                }
                else if (i == 6)
                {
                    Holder.SPRITE_BATCH.DrawString(OptionFont, Options[i], new Vector2(Holder.WIDTH / 2 - LeftOffset, Begin + i * TopOffset), OptionColors[i]);
                    Holder.SPRITE_BATCH.DrawString(OptionFont, ((int)(Holder.SETTINGS.LastEffectsVolume * 10)).ToString(), new Vector2(Holder.WIDTH / 2 + RightOffset, Begin + i * TopOffset), OptionColors[i]);
                }
            }
        }

        private void ReturnFrom()
        {
            Holder.MENUMODE = 0;

            Holder.PLAYERNAME = Holder.SETTINGS.LastSavedPilotName;
            WeaponType = Holder.SETTINGS.LastWeaponType;
            Difficulity = Holder.SETTINGS.LastDifficulty;
            MusicVol = Holder.SETTINGS.LastMusicVolume;
            Holder.SETTINGS.LastEffectsVolume = SaveEffectsVol;
            MediaPlayer.Volume = Holder.SETTINGS.LastMusicVolume;
        }

        private void SaveSettings()
        {
            Holder.MENUMODE = 0;
            if(MusicVol > 1.0f) MusicVol = 1.0f;

            Holder.SETTINGS.LastSavedPilotName = Holder.PLAYERNAME;
            Holder.SETTINGS.LastWeaponType = WeaponType;
            Holder.SETTINGS.LastDifficulty = Difficulity;
            Holder.SETTINGS.LastMusicVolume = MusicVol;

            string gameFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SIPP");
            string settingsPath = Path.Combine(gameFolder, "settings.json");
            string json = JsonSerializer.Serialize(Holder.SETTINGS, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(settingsPath, json);
        }
    }
}
