using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SpaceInvaderPlusPlus.Utilities;
using System;
using System.Threading;

namespace SpaceInvaderPlusPlus
{
    public class SpaceInvaderHandler : Game
    {
        public General _general;
        private GraphicsDeviceManager _graphics;
        private SpaceBackground spaceBackground;
        private MainWorld world;
        private MainMenu menu;
        private Song bg_music_menu;
        private Song bg_music_combat;
        private string music_menu;
        private string music_combat;

        public SpaceInvaderHandler()
        {
            _general = new General();
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.IsBorderless = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _general.RANDOM = new Random();
            _general.WIDTH = 1000;
            _general.HEIGHT = 900;
            _general.SCALE = 1.0f;
            _general.STARTNEW = false;
            _general.MENUMODE = 0;
            _general.RUNWORLD = false;
            _general.CONTENT = this.Content;
            _general.SPRITE_BATCH = new SpriteBatch(GraphicsDevice);

            spaceBackground = new SpaceBackground(ref _general);
            world = new MainWorld(ref _general);
            menu = new MainMenu(Window, ref _general);

            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = _general.WIDTH;
            _graphics.PreferredBackBufferHeight = _general.HEIGHT;
            _graphics.ApplyChanges();

            music_menu = "music/menu";
            music_combat = "music/combat";
            bg_music_menu = _general.CONTENT.Load<Song>(music_menu);
            bg_music_combat = _general.CONTENT.Load<Song>(music_combat);
            MediaPlayer.Play(bg_music_menu);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = _general.SETTINGS.LastMusicVolume;
        }

        protected override void Update(GameTime gameTime)
        {
            _general.KSTATE = Keyboard.GetState();

            if (_general.MENUMODE == 3) Exit();
            if (_general.STARTNEW)
            {
                world.RanNew(ref _general);
            }
            Thread BgThread = new Thread(x => spaceBackground.Update(ref _general));
            BgThread.Start();

            if (!_general.RUNWORLD)
            {
                if ("music/" + MediaPlayer.Queue.ActiveSong.Name != music_menu)
                    MediaPlayer.Play(bg_music_menu);
                menu.Update(ref gameTime, ref _general);
            }
            else
            {
                if ("music/" + MediaPlayer.Queue.ActiveSong.Name != music_combat)
                    MediaPlayer.Play(bg_music_combat);
                world.Update(ref gameTime, ref _general);
            }

            _general.KSTATE_PREV = _general.KSTATE;
            BgThread.Join();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _general.SPRITE_BATCH.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            spaceBackground.DrawAll(ref _general);

            if (!_general.RUNWORLD)
                menu.Draw(ref _general);
            else
                world.Draw(ref _general);

            _general.SPRITE_BATCH.End();

            base.Draw(gameTime);
        }
    }
}
