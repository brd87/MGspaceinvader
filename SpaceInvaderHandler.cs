using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceInvaderPlusPlus.Enemies;
using SpaceInvaderPlusPlus.Weapons;
using System;
using System.Collections.Generic;

namespace SpaceInvaderPlusPlus
{
    public class SpaceInvaderHandler : Game
    {
        private GraphicsDeviceManager _graphics;
        private MainWorld world;

        public SpaceInvaderHandler()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.IsBorderless = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Holder.random = new Random();
            Holder.width = 1000;
            Holder.height = 900;
            Holder.scale = 1.0f;
            world = new MainWorld();

            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = Holder.width;
            _graphics.PreferredBackBufferHeight = Holder.height;
            _graphics.ApplyChanges();

            // END
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here
            Holder.content = this.Content;
            Holder.spriteBatch = new SpriteBatch(GraphicsDevice);

            world.LoadContent(1);

            // END
        }

        protected override void Update(GameTime gameTime)
        {


            Holder.kState = Keyboard.GetState();
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            world.Update(gameTime);

            // END
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            world.Draw();

            // END
            base.Draw(gameTime);
        }
    }
}
