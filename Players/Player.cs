using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SpaceInvaderPlusPlus.Players
{
    public class Player : Entity
    {
        PlayerPart PlFront;
        PlayerPart PlLeft;
        PlayerPart PlRight;

        public int Health { get; set; }
        public int Shields { get; set; }

        public float FrontAcceleration { get; set; }
        public float SideAcceleration { get; set; }
        public float BackAcceleration { get; set; }
        public float Stabilisers { get; set; }
        public float Drag { get; set; }

        public bool AskToFire { get; set; }

        public Vector2 Velocity;

        public Player(Vector2 position, float angle = 0.0f, string spriteName = "player", int entityLayer = 1) :
            base(position, angle, spriteName, entityLayer)
        {
            PlFront = new PlayerPart(new Vector2(Holder.WIDTH / 2, Holder.HEIGHT / 4 * 3), "player_front");
            PlLeft = new PlayerPart(new Vector2(Holder.WIDTH / 2, Holder.HEIGHT / 4 * 3), "player_lwing");
            PlRight = new PlayerPart(new Vector2(Holder.WIDTH / 2, Holder.HEIGHT / 4 * 3), "player_rwing");

            Velocity = new Vector2(0.0f, 0.0f);

            Health = 100;
            Shields = 100;
            FrontAcceleration = 0.5f;
            SideAcceleration = 0.3f;
            BackAcceleration = 0.4f;
            Stabilisers = 0.1f;
            Drag = 0.2f;
            if (Holder.SETTINGS.LastDifficulty == 0)
            {
                Health *= 2;
                Shields *= 2;
                FrontAcceleration *= 1.2f;
                SideAcceleration *= 1.5f;
                BackAcceleration *= 1.2f;
                Stabilisers *= 1.5f;
            }
            else if (Holder.SETTINGS.LastDifficulty == 1)
            {
                //defoult movement
            }
            else if (Holder.SETTINGS.LastDifficulty == 2)
            {
                Health = 75;
                Shields = 75;
                FrontAcceleration *= 0.75f;
                SideAcceleration *= 0.75f;
                BackAcceleration *= 0.75f;
                Stabilisers *= 0.5f;
            }
            else
            {
                Health = 50;
                Shields = 50;
                FrontAcceleration *= 0.6f;
                SideAcceleration *= 0.5f;
                BackAcceleration *= 0.6f;
                Stabilisers *= 0.3f;
            }

        }

        public void Update()
        {
            AskToFire = false;
            if (Holder.KSTATE.IsKeyDown(Keys.Space))
            {
                AskToFire = true;
            }

            //Input Down
            if (Holder.KSTATE.IsKeyDown(Keys.W))
            {
                Velocity.Y -= FrontAcceleration;
                Position += Velocity;
            }
            if (Holder.KSTATE.IsKeyDown(Keys.S))
            {
                Velocity.Y += BackAcceleration;
                Position += Velocity;
            }
            if (Holder.KSTATE.IsKeyDown(Keys.A))
            {
                Velocity.X -= SideAcceleration;
                Position += Velocity;
                if (Angle > -0.2f)
                    Angle -= 0.01f;
            }
            if (Holder.KSTATE.IsKeyDown(Keys.D))
            {
                Velocity.X += SideAcceleration;
                Position += Velocity;
                if (Angle < 0.2f)
                    Angle += 0.01f;
            }

            //Input Up
            if (Holder.KSTATE.IsKeyUp(Keys.Space))
            {
                AskToFire = false;
            }

            if (Holder.KSTATE.IsKeyUp(Keys.W) && Holder.KSTATE.IsKeyUp(Keys.S))
            {
                if (Velocity.Y < 0.3f)
                    Velocity.Y += Drag;
                if (Velocity.Y > -0.2f)
                    Velocity.Y -= Stabilisers;
                Position += Velocity;
            }
            if (Holder.KSTATE.IsKeyUp(Keys.A) && Holder.KSTATE.IsKeyUp(Keys.D))
            {
                if (Velocity.X > 0.3f)
                    Velocity.X -= Stabilisers;
                if (Velocity.X < -0.3f)
                    Velocity.X += Stabilisers;
                if (Angle > 0.0f)
                    Angle -= 0.01f;
                if (Angle < 0.0f)
                    Angle += 0.01f;
                Position += Velocity;
            }

            //COR
            if (Position.X < 0)
            {
                Position.X = 0;
                Velocity.X = 0;
            }

            if (Position.X > Holder.WIDTH)
            {
                Position.X = Holder.WIDTH;
                Velocity.X = 0;
            }

            if (Position.Y < 0)
            {
                Position.Y = 0;
                Velocity.Y = 3;
            }

            if (Position.Y > Holder.HEIGHT)
            {
                Position.Y = Holder.HEIGHT;
                Velocity.Y = 0;
            }

            PlFront.Update(Position);
            PlLeft.Update(Position);
            PlRight.Update(Position);
        }

        public void DrawAll()
        {
            PlFront.DrawEntity();
            PlLeft.DrawEntity();
            PlRight.DrawEntity();
            DrawEntity();
        }

        public void PlayerDamage(int damage)
        {
            if (Shields > 0)
            {
                Shields -= damage;
                if (Shields < 0)
                {
                    Health += Shields;
                    Shields = 0;
                }
                return;
            }
            Health -= damage;
            if (Health < 0) Health = 0;
        }
    }
}
