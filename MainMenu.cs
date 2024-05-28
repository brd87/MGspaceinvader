using Microsoft.Xna.Framework;
using SpaceInvaderPlusPlus.Menus;
using SpaceInvaderPlusPlus.Utilities;
using System;
using System.IO;
using System.Text.Json;

namespace SpaceInvaderPlusPlus
{
    public class MainMenu
    {
        //private PauseMenu _pauseMenu; //[0] pause, quit(?), exit
        private TitleMenu _titleMenu; //[1] play, scoreboard, settings, exit
        private ScoreboardMenu _scoreboardMenu; //[2] rank(1-20) - pilotname - score
        private SettingsMenu _settingsMenu; //[3] dif, weapon, pilotname + eventual sound settings
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
                Holder.SETTINGS = new Settings("John Invader", 1, 1);
                string json = JsonSerializer.Serialize(Holder.SETTINGS, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(settingsPath, json);
            }

            if (File.Exists(topPath))
                Holder.TOP_PLAYERS = JsonSerializer.Deserialize<TopPlayers>(File.ReadAllText(topPath));

            //ini all menus
            _titleMenu = new TitleMenu();
            _scoreboardMenu = new ScoreboardMenu();
            _settingsMenu = new SettingsMenu(gameWindow);
        }

        public void Update(GameTime gameTime)
        {
            //using ifs with Holder.menuMode update given menu with it's Update
            if (Holder.MENUMODE == 0)
                _titleMenu.Update(gameTime);
            else if (Holder.MENUMODE == 1)
                _scoreboardMenu.Update(gameTime);
            else if (Holder.MENUMODE == 2)
                _settingsMenu.Update(gameTime);
        }

        public void Draw()
        {
            //like in update but for drawing
            if (Holder.MENUMODE == 0)
                _titleMenu.Draw();
            else if (Holder.MENUMODE == 1)
                _scoreboardMenu.Draw();
            else if (Holder.MENUMODE == 2)
                _settingsMenu.Draw();
        }
    }
}
