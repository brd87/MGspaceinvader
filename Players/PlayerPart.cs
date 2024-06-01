using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SpaceInvaderPlusPlus.Players
{
    internal class PlayerPart : Entity
    {
        private int id { get; set; }
        public PlayerPart(Vector2 position, string spriteName, float angle = 0.0f, int entityLayer = 1) : base(position, angle, spriteName, entityLayer)
        {
            if (spriteName == "player/player_lwing") id = 0;
            if (spriteName == "player/player_front") id = 1;
            if (spriteName == "player/player_rwing") id = 2;
        }

        public void Update(Vector2 shipPosition)
        {
            Position = shipPosition;

            if ((id == 1 || id == 0) && Holder.KSTATE.IsKeyDown(Keys.A) && Angle > -0.2f)
                Angle -= 0.01f;
            if ((id == 1 || id == 2) && Holder.KSTATE.IsKeyDown(Keys.D) && Angle < 0.2f)
                Angle += 0.01f;

            if (id == 2 && Holder.KSTATE.IsKeyDown(Keys.A) && Angle > -0.4f)
                Angle -= 0.02f;
            if (id == 0 && Holder.KSTATE.IsKeyDown(Keys.D) && Angle < 0.4f)
                Angle += 0.02f;

            if (id == 0 && Holder.KSTATE.IsKeyDown(Keys.W) && Angle > -0.4f)
                Angle -= 0.02f;
            if (id == 0 && Holder.KSTATE.IsKeyDown(Keys.S) && Angle < 0.4f)
                Angle += 0.02f;

            if (id == 2 && Holder.KSTATE.IsKeyDown(Keys.W) && Angle < 0.4f)
                Angle += 0.02f;
            if (id == 2 && Holder.KSTATE.IsKeyDown(Keys.S) && Angle > -0.4f)
                Angle -= 0.02f;

            if (Holder.KSTATE.IsKeyUp(Keys.A) && Holder.KSTATE.IsKeyUp(Keys.D) && Holder.KSTATE.IsKeyUp(Keys.W) && Holder.KSTATE.IsKeyUp(Keys.S))
            {
                if (Angle > 0.0f)
                    Angle -= 0.01f;
                if (Angle < 0.0f)
                    Angle += 0.01f;
            }
        }
    }
}
