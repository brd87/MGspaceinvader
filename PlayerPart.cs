using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaderPlusPlus
{
    internal class PlayerPart : Entity
    {
        private string SpriteName;
        public PlayerPart(Vector2 position, string spriteName, float angle = 0.0f,  int entityLayer = 1) : base(position, angle, spriteName, entityLayer) 
        {
            SpriteName = spriteName;
        }

        public void Update(Vector2 shipPosition, KeyboardState kState)
        {
            this.Position = shipPosition;

            if ((SpriteName == "player_front" || SpriteName == "player_lwing") && kState.IsKeyDown(Keys.A) && Angle > -0.2f)
                Angle -= 0.01f;
            if ((SpriteName == "player_front" || SpriteName == "player_rwing") && kState.IsKeyDown(Keys.D) && Angle < 0.2f)
                Angle += 0.01f;

            if (SpriteName == "player_rwing" && kState.IsKeyDown(Keys.A) && Angle > -0.4f)
                Angle -= 0.02f;
            if (SpriteName == "player_lwing" && kState.IsKeyDown(Keys.D) && Angle < 0.4f)
                Angle += 0.02f;

            if (SpriteName == "player_lwing" && kState.IsKeyDown(Keys.W) && Angle > -0.4f)
                Angle -= 0.02f;
            if (SpriteName == "player_lwing" && kState.IsKeyDown(Keys.S) && Angle < 0.4f)
                Angle += 0.02f;

            if (SpriteName == "player_rwing" && kState.IsKeyDown(Keys.W) && Angle < 0.4f)
                Angle += 0.02f;
            if (SpriteName == "player_rwing" && kState.IsKeyDown(Keys.S) && Angle > -0.4f)
                Angle -= 0.02f;

            if (kState.IsKeyUp(Keys.A) && kState.IsKeyUp(Keys.D) && kState.IsKeyUp(Keys.W) && kState.IsKeyUp(Keys.S))
            {
                if (Angle > 0.0f)
                    Angle -= 0.01f;
                if (Angle < 0.0f)
                    Angle += 0.01f;
            }
        }
    }
}
