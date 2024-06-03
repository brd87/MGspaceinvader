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
        private float Cooldawn;
        private TimeSpan LastTime;
        private SpriteFont OptionFont;
        private List<string> Options;
        private int LeftOffset;
        private int RightOffset;
        private int TopOffset;
        private int Begin;
        private List<Vector2> OptionOffsets;
        private List<Color> OptionColors;
        private int CurrentSelected;
        private int WeaponType;
        private int Difficulity;
        private float MusicVol;
        private float SaveEffectsVol;
        private List<string> WeaponNames;
        private List<string> DifficulityNames;
        private TextInputHandler TextInput;

        public SettingsMenu(ref General general, GameWindow gameWindow)
        {
            Cooldawn = 0.2f;
            LastTime = TimeSpan.FromSeconds(0.0f);

            OptionFont = general.CONTENT.Load<SpriteFont>("font/font_options");
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

            general.PLAYERNAME = general.SETTINGS.LastSavedPilotName;
            WeaponType = general.SETTINGS.LastWeaponType;
            Difficulity = general.SETTINGS.LastDifficulty;
            MusicVol = general.SETTINGS.LastMusicVolume;
            SaveEffectsVol = general.SETTINGS.LastEffectsVolume;

            WeaponNames = new List<string> { "A-100 \"Warpig\"", "DS-X \"Smart Diax\"", "Rg50 \"Shashlik\"" };
            DifficulityNames = new List<string> { "Stable (EASY)", "Containgus (MEDIUM)", "Colapse (HARD)", "Quasimorphosis (HARDER)" };



            LeftOffset = 300;
            RightOffset = 25;
            TopOffset = 38;
            Begin = 400;

            TextInput = new TextInputHandler("^[a-zA-Z0-9_<>-]$", ref gameWindow);
            TextInput.SetGeneralInstance(ref general);
        }

        public void Update(ref General general, GameTime gameTime)
        {
            if (gameTime.TotalGameTime - LastTime >= TimeSpan.FromSeconds(Cooldawn))
                if (!TextInput.IsTextInputActive)
                {
                    if (general.KSTATE.IsKeyDown(Keys.W))
                    {
                        LastTime = gameTime.TotalGameTime;
                        OptionColors[CurrentSelected] = Color.Gray;
                        if (CurrentSelected == 0)
                            CurrentSelected = Options.Count - 1;
                        else
                            CurrentSelected--;
                        OptionColors[CurrentSelected] = Color.White;
                    }
                    else if (general.KSTATE.IsKeyDown(Keys.S))
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
                if (i == CurrentSelected && general.KSTATE.IsKeyDown(Keys.Enter))
                    if (general.KSTATE != general.KSTATE_PREV)
                    {
                        if (CurrentSelected == 0)
                        {
                            ReturnFrom(ref general);
                        }
                        else if (CurrentSelected == 1)
                        {
                            if (general.PLAYERNAME.Length != 0)
                                SaveSettings(ref general);
                            else
                                general.PLAYERNAME = "NAMELESS PILOT";
                        }
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
                            general.SETTINGS.LastEffectsVolume += 0.1f;
                            if (general.SETTINGS.LastEffectsVolume > 1.1) general.SETTINGS.LastEffectsVolume = 0;
                            else if (general.SETTINGS.LastEffectsVolume > 1.0f) general.SETTINGS.LastEffectsVolume = 1.0f;
                        }
                    }
        }

        public void Draw(ref General general)
        {
            for (int i = 0; i < Options.Count; i++)
            {
                if (i < 2)
                {
                    general.SPRITE_BATCH.DrawString(OptionFont, Options[i], new Vector2(general.WIDTH / 2, Begin + i * TopOffset) - OptionOffsets[i], OptionColors[i],
                        0f, Vector2.Zero, 1f, SpriteEffects.None, 1);
                }
                else if (i == 2)
                {
                    general.SPRITE_BATCH.DrawString(OptionFont, Options[i], new Vector2(general.WIDTH / 2 - LeftOffset, Begin + i * TopOffset), OptionColors[i],
                        0f, Vector2.Zero, 1f, SpriteEffects.None, 1);
                    general.SPRITE_BATCH.DrawString(OptionFont, general.PLAYERNAME, new Vector2(general.WIDTH / 2 + RightOffset, Begin + i * TopOffset), OptionColors[i],
                        0f, Vector2.Zero, 1f, SpriteEffects.None, 1);
                }
                else if (i == 3)
                {
                    general.SPRITE_BATCH.DrawString(OptionFont, Options[i], new Vector2(general.WIDTH / 2 - LeftOffset, Begin + i * TopOffset), OptionColors[i],
                        0f, Vector2.Zero, 1f, SpriteEffects.None, 1);
                    general.SPRITE_BATCH.DrawString(OptionFont, WeaponNames[WeaponType], new Vector2(general.WIDTH / 2 + RightOffset, Begin + i * TopOffset), OptionColors[i],
                        0f, Vector2.Zero, 1f, SpriteEffects.None, 1);
                }
                else if (i == 4)
                {
                    general.SPRITE_BATCH.DrawString(OptionFont, Options[i], new Vector2(general.WIDTH / 2 - LeftOffset, Begin + i * TopOffset), OptionColors[i],
                        0f, Vector2.Zero, 1f, SpriteEffects.None, 1);
                    general.SPRITE_BATCH.DrawString(OptionFont, DifficulityNames[Difficulity], new Vector2(general.WIDTH / 2 + RightOffset, Begin + i * TopOffset), OptionColors[i],
                        0f, Vector2.Zero, 1f, SpriteEffects.None, 1);
                }
                else if (i == 5)
                {
                    general.SPRITE_BATCH.DrawString(OptionFont, Options[i], new Vector2(general.WIDTH / 2 - LeftOffset, Begin + i * TopOffset), OptionColors[i],
                        0f, Vector2.Zero, 1f, SpriteEffects.None, 1);
                    general.SPRITE_BATCH.DrawString(OptionFont, ((int)(MusicVol * 10)).ToString(), new Vector2(general.WIDTH / 2 + RightOffset, Begin + i * TopOffset), OptionColors[i],
                        0f, Vector2.Zero, 1f, SpriteEffects.None, 1);
                }
                else if (i == 6)
                {
                    general.SPRITE_BATCH.DrawString(OptionFont, Options[i], new Vector2(general.WIDTH / 2 - LeftOffset, Begin + i * TopOffset), OptionColors[i],
                        0f, Vector2.Zero, 1f, SpriteEffects.None, 1);
                    general.SPRITE_BATCH.DrawString(OptionFont, ((int)(general.SETTINGS.LastEffectsVolume * 10)).ToString(), new Vector2(general.WIDTH / 2 + RightOffset, Begin + i * TopOffset), OptionColors[i],
                        0f, Vector2.Zero, 1f, SpriteEffects.None, 1);
                }
            }
        }

        private void ReturnFrom(ref General general)
        {
            general.MENUMODE = 0;

            general.PLAYERNAME = general.SETTINGS.LastSavedPilotName;
            WeaponType = general.SETTINGS.LastWeaponType;
            Difficulity = general.SETTINGS.LastDifficulty;
            MusicVol = general.SETTINGS.LastMusicVolume;
            general.SETTINGS.LastEffectsVolume = SaveEffectsVol;
            MediaPlayer.Volume = general.SETTINGS.LastMusicVolume;
        }

        private void SaveSettings(ref General general)
        {
            general.MENUMODE = 0;
            if (MusicVol > 1.0f) MusicVol = 1.0f;

            general.SETTINGS.LastSavedPilotName = general.PLAYERNAME;
            general.SETTINGS.LastWeaponType = WeaponType;
            general.SETTINGS.LastDifficulty = Difficulity;
            general.SETTINGS.LastMusicVolume = MusicVol;

            string gameFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SIPP");
            string settingsPath = Path.Combine(gameFolder, "settings.json");
            string json = JsonSerializer.Serialize(general.SETTINGS, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(settingsPath, json);
        }
    }
}
