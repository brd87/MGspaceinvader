using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceInvaderPlusPlus.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace SpaceInvaderPlusPlus.Menus
{
    internal class SettingsMenu
    {
        public float Cooldawn { get; set; }
        public TimeSpan LastTime { get; set; }
        public SpriteFont OptionFont { get; set; }
        public List<string> Options { get; set; }
        public Vector2 TitleOffset { get; set; }
        public Vector2 SubTitleOffset { get; set; }
        private int LeftOffset { get; set; }
        public int RightOffset { get; set; }
        public int TopOffset { get; set; }
        public int Begin { get; set; }
        public List<Vector2> OptionOffsets { get; set; }
        public List<Color> OptionColors { get; set; }
        public int CurrentSelected { get; set; }
        public int WeaponType { get; set; }
        public int Difficulity { get; set; }
        public List<string> WeaponNames { get; set; }
        public List<string> DifficulityNames { get; set; }
        public TextInputHandler TextInput { get; set; }

        public SettingsMenu(GameWindow gameWindow)
        {
            Cooldawn = 0.2f;
            LastTime = TimeSpan.FromSeconds(0.0f);

            OptionFont = Holder.CONTENT.Load<SpriteFont>("font_options");
            Options = new List<string>() { "RETURN", "APPLY AND SAVE", "Pilot Name", "Weapon System", "Difficulity" };
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

            WeaponNames = new List<string> { "A-100 \"Warpig\"", "KD-406 \"Drilljectile\"", "Rg50 \"Project Spearman\"" };
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
            }
        }

        private void ReturnFrom()
        {
            Holder.MENUMODE = 0;
            Holder.PLAYERNAME = Holder.SETTINGS.LastSavedPilotName;
            WeaponType = Holder.SETTINGS.LastWeaponType;
            Difficulity = Holder.SETTINGS.LastDifficulty;
        }

        private void SaveSettings()
        {
            Holder.MENUMODE = 0;
            Holder.SETTINGS.LastSavedPilotName = Holder.PLAYERNAME;
            Holder.SETTINGS.LastWeaponType = WeaponType;
            Holder.SETTINGS.LastDifficulty = Difficulity;

            string gameFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SIPP");
            string settingsPath = Path.Combine(gameFolder, "settings.json");
            string json = JsonSerializer.Serialize(Holder.SETTINGS, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(settingsPath, json);
        }
    }
}
