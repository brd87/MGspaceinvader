using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceInvaderPlusPlus.Utilities;
using System;

namespace SpaceInvaderPlusPlus
{
    public class SpaceInvaderHandler : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpaceBackground spaceBackground;
        private MainWorld world;
        private MainMenu menu;

        public SpaceInvaderHandler()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.IsBorderless = true;
        }

        protected override void Initialize()
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

            base.Initialize();
        }

        protected override void LoadContent() { }

        protected override void Update(GameTime gameTime)
        {
            Holder.KSTATE = Keyboard.GetState();
            Holder.MSTATE = Mouse.GetState();

            if (Holder.MENUMODE == 3) Exit();

            spaceBackground.Update();
            if (Holder.STARTNEW) world.RanNew();


            if (!Holder.RUNWORLD) menu.Update(gameTime);
            else world.Update(gameTime);

            Holder.KSTATE_PREV = Holder.KSTATE;
            Holder.MSTATE_PREV = Holder.MSTATE;

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
