using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SpaceInvaderPlusPlus.Players
{
    internal class PlayerPart : Entity
    {
        private int id { get; set; }
        public PlayerPart(ref General general, Vector2 position, string spriteName, float angle = 0.0f, int entityLayer = 1) : base(ref general, position, angle, spriteName, entityLayer)
        {
            if (spriteName == "player/player_lwing") id = 0;
            if (spriteName == "player/player_front") id = 1;
            if (spriteName == "player/player_rwing") id = 2;
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
