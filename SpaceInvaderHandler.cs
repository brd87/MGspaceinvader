using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        private GraphicsDeviceManager _graphics { get; set; }
        private SpaceBackground spaceBackground {  get; set; }
        private MainWorld world {  get; set; }
        private MainMenu menu {  get; set; }
        private Song bg_music_menu {  get; set; }
        private Song bg_music_combat { get; set; }
        private string music_menu {  get; set; }
        private string music_combat {  get; set; }

        public SpaceInvaderHandler()
        {
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
            Holder.RANDOM = new Random();
            Holder.WIDTH = 1000;
            Holder.HEIGHT = 900;
            Holder.SCALE = 1.0f;
            Holder.STARTNEW = false;
            Holder.MENUMODE = 0;
            Holder.RUNWORLD = false;
            Holder.CONTENT = this.Content;
            Holder.SPRITE_BATCH = new SpriteBatch(GraphicsDevice);

            spaceBackground = new SpaceBackground();
            world = new MainWorld();
            menu = new MainMenu(Window);

            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = Holder.WIDTH;
            _graphics.PreferredBackBufferHeight = Holder.HEIGHT;
            _graphics.ApplyChanges();

            music_menu = "music/menu";
            music_combat = "music/combat";
            bg_music_menu = Holder.CONTENT.Load<Song>(music_menu);
            bg_music_combat = Holder.CONTENT.Load<Song>(music_combat);
            MediaPlayer.Play(bg_music_menu);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = Holder.SETTINGS.LastMusicVolume;
        }

        protected override void Update(GameTime gameTime)
        {
            Holder.KSTATE = Keyboard.GetState();
            Holder.MSTATE = Mouse.GetState();

            if (Holder.MENUMODE == 3) Exit();
            if (Holder.STARTNEW)
            {
                world.RanNew();
                MediaPlayer.Play(bg_music_combat);
            }
            Thread BgThread = new Thread(x => spaceBackground.Update());
            BgThread.Start();

            if (!Holder.RUNWORLD) menu.Update(gameTime);
            else world.Update(gameTime);

            Holder.KSTATE_PREV = Holder.KSTATE;
            Holder.MSTATE_PREV = Holder.MSTATE;
            BgThread.Join();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            Holder.SPRITE_BATCH.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            spaceBackground.DrawAll();

            if (!Holder.RUNWORLD)
                menu.Draw();
            else
                world.Draw();

            Holder.SPRITE_BATCH.End();

            base.Draw(gameTime);
        }
    }
}
