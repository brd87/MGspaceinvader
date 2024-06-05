using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SpaceInvaderPlusPlus.Utilities;
using System;
using System.Threading;

namespace SpaceInvaderPlusPlus
{
    internal class SpaceInvaderHandler : Game
    {
        public General _general;
        private GraphicsDeviceManager _graphics;
        private SpaceBackground _spaceBackground;
        private MainWorld _world;
        private MainMenu _menu;
        private string _music_menu;
        private string _music_combat;

        public SpaceInvaderHandler()
        {
            _general = new General();
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
            Window.IsBorderless = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {

            _general.RANDOM = new Random();
            _general.WIDTH = GraphicsDevice.DisplayMode.Width;
            _general.HEIGHT = GraphicsDevice.DisplayMode.Height;
            _general.SCALE = 1.0f;
            _general.GAMESTATE = 0;
            _general.MENUMODE = 0;
            _general.CONTENT = this.Content;
            _general.SPRITE_BATCH = new SpriteBatch(GraphicsDevice);
            _general.ASSETLIBRARY = new AssetLibrary(ref _general);

            _spaceBackground = new SpaceBackground(ref _general);
            _world = new MainWorld(ref _general);
            _menu = new MainMenu(Window, ref _general);

            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = _general.WIDTH;
            _graphics.PreferredBackBufferHeight = _general.HEIGHT;
            _graphics.ApplyChanges();

            _music_menu = "music/menu";
            _music_combat = "music/combat";
            MediaPlayer.Play(_general.ASSETLIBRARY.bg_music_menu);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = _general.SETTINGS.LastMusicVolume;
        }

        protected override void Update(GameTime gameTime)
        {
            _general.KSTATE = Keyboard.GetState();

            Thread BgThread = new Thread(x => _spaceBackground.Update(ref _general));
            BgThread.Start();

            if (_general.GAMESTATE == 0)
            {
                if ("music/" + MediaPlayer.Queue.ActiveSong.Name != _music_menu)
                    MediaPlayer.Play(_general.ASSETLIBRARY.bg_music_menu);
                _menu.Update(ref gameTime, ref _general);
            }
            else if (_general.GAMESTATE == 1)
            {
                if ("music/" + MediaPlayer.Queue.ActiveSong.Name != _music_combat)
                    MediaPlayer.Play(_general.ASSETLIBRARY.bg_music_combat);
                _world.RanNew(ref _general);
                _general.GAMESTATE = 2;
            }
            else if (_general.GAMESTATE == 2)
            {
                _world.Update(ref gameTime, ref _general);
            }
            else if (_general.GAMESTATE == 3)
            {
                _world.CleanUp();
                _general.GAMESTATE = 0;
            }
            else
            {
                Exit();
            }

            _general.KSTATE_PREV = _general.KSTATE;
            BgThread.Join();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _general.SPRITE_BATCH.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

            _spaceBackground.DrawAll(ref _general);

            if (_general.GAMESTATE == 0)
                _menu.Draw(ref _general);
            else if (_general.GAMESTATE == 2)
                _world.Draw(ref _general);

            _general.SPRITE_BATCH.End();

            base.Draw(gameTime);
        }
    }
}
