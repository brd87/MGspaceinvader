using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceInvaderPlusPlus.Menus;
using SpaceInvaderPlusPlus.Utilities;
using System;
using System.IO;
using System.Text.Json;

namespace SpaceInvaderPlusPlus
{
    internal class MainMenu
    {
        private TitleMenu _titleMenu;
        private ScoreboardMenu _scoreboardMenu;
        private SettingsMenu _settingsMenu;
        private SpriteFont _gameVersionFont;
        private Vector2 _gameVersionOffset;
        private string _gameVersion;
        public MainMenu(GameWindow gameWindow, ref General general)
        {
            general.TOP_PLAYERS = new TopPlayers();

            string gameFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SIPP");
            string settingsPath = Path.Combine(gameFolder, "settings.json");
            string topPath = Path.Combine(gameFolder, "top.json");
            if (!Directory.Exists(gameFolder))
                Directory.CreateDirectory(gameFolder);

            if (File.Exists(settingsPath))
                general.SETTINGS = JsonSerializer.Deserialize<Settings>(File.ReadAllText(settingsPath));
            else
            {
                general.SETTINGS = new Settings("John Invader", 1, 1, 0.5f, 0.5f);
                string json = JsonSerializer.Serialize(general.SETTINGS, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(settingsPath, json);
            }

            if (File.Exists(topPath))
                general.TOP_PLAYERS = JsonSerializer.Deserialize<TopPlayers>(File.ReadAllText(topPath));

            _titleMenu = new TitleMenu(ref general);
            _scoreboardMenu = new ScoreboardMenu(ref general);
            _settingsMenu = new SettingsMenu(ref general, gameWindow);

            _gameVersion = "S.I.P.P (v0.9.53)";
            _gameVersionFont = general.CONTENT.Load<SpriteFont>("font/font_hudaux");
            _gameVersionOffset = _gameVersionFont.MeasureString(_gameVersion) / 2;
        }

        public void Update(ref GameTime gameTime, ref General general)
        {
            if (general.KSTATE.IsKeyDown(Keys.Enter))
                if (general.KSTATE != general.KSTATE_PREV)
                {
                    SoundEffectInstance select = general.ASSETLIBRARY.eff_Select.CreateInstance();
                    select.Volume = general.SETTINGS.LastEffectsVolume;
                    select.Play();
                }


            if (general.MENUMODE == 0)
                _titleMenu.Update(ref general, gameTime);
            else if (general.MENUMODE == 1)
                _scoreboardMenu.Update(ref general, gameTime);
            else if (general.MENUMODE == 2)
                _settingsMenu.Update(ref general, gameTime);
        }

        public void Draw(ref General general)
        {
            if (general.MENUMODE == 0)
                _titleMenu.Draw(ref general);
            else if (general.MENUMODE == 1)
                _scoreboardMenu.Draw(ref general);
            else if (general.MENUMODE == 2)
                _settingsMenu.Draw(ref general);

            general.SPRITE_BATCH.DrawString(_gameVersionFont, _gameVersion, new Vector2(general.WIDTH / 2, 850) - _gameVersionOffset, Color.Gray,
                0f, Vector2.Zero, 1f, SpriteEffects.None, 1);
        }
    }
}
