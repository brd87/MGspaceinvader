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
    public class MainMenu
    {
        private TitleMenu _titleMenu { get; set; }
        private ScoreboardMenu _scoreboardMenu { get; set; }
        private SettingsMenu _settingsMenu { get; set; }
        private SpriteFont _gameVersionFont { get; set; }
        private Vector2 _gameVersionOffset { get; set; }
        private string _gameVersion { get; set; }
        private SoundEffect _menuSoundEffectIns { get; set; }
        public MainMenu(GameWindow gameWindow)
        {
            Holder.TOP_PLAYERS = new TopPlayers();

            string gameFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SIPP");
            string settingsPath = Path.Combine(gameFolder, "settings.json");
            string topPath = Path.Combine(gameFolder, "top.json");
            if (!Directory.Exists(gameFolder))
                Directory.CreateDirectory(gameFolder);

            if (File.Exists(settingsPath))
                Holder.SETTINGS = JsonSerializer.Deserialize<Settings>(File.ReadAllText(settingsPath));
            else
            {
                Holder.SETTINGS = new Settings("John Invader", 1, 1, 0.5f, 0.5f);
                string json = JsonSerializer.Serialize(Holder.SETTINGS, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(settingsPath, json);
            }

            if (File.Exists(topPath))
                Holder.TOP_PLAYERS = JsonSerializer.Deserialize<TopPlayers>(File.ReadAllText(topPath));

            _titleMenu = new TitleMenu();
            _scoreboardMenu = new ScoreboardMenu();
            _settingsMenu = new SettingsMenu(gameWindow);

            _gameVersion = "S.I.P.P (v0.9.13)";
            _gameVersionFont = Holder.CONTENT.Load<SpriteFont>("font/font_hudaux");
            _gameVersionOffset = _gameVersionFont.MeasureString(_gameVersion) / 2;

            _menuSoundEffectIns = Holder.CONTENT.Load<SoundEffect>("eff/eff_select");
        }

        public void Update(GameTime gameTime)
        {
            if (Holder.KSTATE.IsKeyDown(Keys.Enter))
                if (Holder.KSTATE != Holder.KSTATE_PREV)
                {
                    SoundEffectInstance select = _menuSoundEffectIns.CreateInstance();
                    select.Volume = Holder.SETTINGS.LastEffectsVolume;
                    select.Play();
                }
                    

            if (Holder.MENUMODE == 0)
                _titleMenu.Update(gameTime);
            else if (Holder.MENUMODE == 1)
                _scoreboardMenu.Update(gameTime);
            else if (Holder.MENUMODE == 2)
                _settingsMenu.Update(gameTime);
        }

        public void Draw()
        {
            if (Holder.MENUMODE == 0)
                _titleMenu.Draw();
            else if (Holder.MENUMODE == 1)
                _scoreboardMenu.Draw();
            else if (Holder.MENUMODE == 2)
                _settingsMenu.Draw();

            Holder.SPRITE_BATCH.DrawString(_gameVersionFont, _gameVersion, new Vector2(Holder.WIDTH / 2, 850) - _gameVersionOffset, Color.Gray);
        }
    }
}
