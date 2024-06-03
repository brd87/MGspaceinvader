using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceInvaderPlusPlus.Players
{
    internal class PlayerPart : Entity
    {
        private int id { get; set; }
        public PlayerPart(ref General general, Vector2 position, Texture2D spriteName, float angle = 0.0f, float? scale = null, float spriteLayer = 0.92f) : base(ref general, position, angle, spriteName, scale, spriteLayer)
        {
            if (spriteName == general.ASSETLIBRARY.tPlayer_Left) id = 0;
            if (spriteName == general.ASSETLIBRARY.tPlayer_Front) id = 1;
            if (spriteName == general.ASSETLIBRARY.tPlayer_Right) id = 2;
        }

        public void Update(ref General general, ref Vector2 shipPosition)
        {
            Position = shipPosition;

            if ((id == 1 || id == 0) && general.KSTATE.IsKeyDown(Keys.A) && Angle > -0.2f)
                Angle -= 0.01f;
            if ((id == 1 || id == 2) && general.KSTATE.IsKeyDown(Keys.D) && Angle < 0.2f)
                Angle += 0.01f;

            if (id == 2 && general.KSTATE.IsKeyDown(Keys.A) && Angle > -0.4f)
                Angle -= 0.02f;
            if (id == 0 && general.KSTATE.IsKeyDown(Keys.D) && Angle < 0.4f)
                Angle += 0.02f;

            if (id == 0 && general.KSTATE.IsKeyDown(Keys.W) && Angle > -0.4f)
                Angle -= 0.02f;
            if (id == 0 && general.KSTATE.IsKeyDown(Keys.S) && Angle < 0.4f)
                Angle += 0.02f;

            if (id == 2 && general.KSTATE.IsKeyDown(Keys.W) && Angle < 0.4f)
                Angle += 0.02f;
            if (id == 2 && general.KSTATE.IsKeyDown(Keys.S) && Angle > -0.4f)
                Angle -= 0.02f;

            if (general.KSTATE.IsKeyUp(Keys.A) && general.KSTATE.IsKeyUp(Keys.D) && general.KSTATE.IsKeyUp(Keys.W) && general.KSTATE.IsKeyUp(Keys.S))
            {
                if (Angle > 0.0f)
                    Angle -= 0.01f;
                if (Angle < 0.0f)
                    Angle += 0.01f;
            }
        }
    }
}
