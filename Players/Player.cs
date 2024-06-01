using Microsoft.Xna.Framework.Input;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace SpaceInvaderPlusPlus.Players
{
    public class Player : Entity
    {
        private PlayerPart PlFront { get; set; }
        private PlayerPart PlLeft { get; set; }
        private PlayerPart PlRight { get; set; }

        public int Health { get; set; }
        public int Shields { get; set; }

        private float FrontAcceleration { get; set; }
        private float SideAcceleration { get; set; }
        private float BackAcceleration { get; set; }
        private float Stabilisers { get; set; }
        private float Drag { get; set; }

        public bool AskToFire { get; set; }
        public bool UltAbility { get; set; }

        public Player(Vector2 position, float angle = 0.0f, string spriteName = "player/player", int entityLayer = 1) :
            base(position, angle, spriteName, entityLayer)
        {
            PlFront = new PlayerPart(new Vector2(Holder.WIDTH / 2, Holder.HEIGHT / 4 * 3), "player/player_front");
            PlLeft = new PlayerPart(new Vector2(Holder.WIDTH / 2, Holder.HEIGHT / 4 * 3), "player/player_lwing");
            PlRight = new PlayerPart(new Vector2(Holder.WIDTH / 2, Holder.HEIGHT / 4 * 3), "player/player_rwing");

            Health = 100;
            Shields = 100;
            FrontAcceleration = 0.6f;
            SideAcceleration = 0.4f;
            BackAcceleration = 0.5f;
            Stabilisers = 0.3f;
            Drag = 0.2f;
            if (Holder.SETTINGS.LastDifficulty == 0)
            {
                Health *= 2;
                Shields *= 2;
                FrontAcceleration *= 1.2f;
                SideAcceleration *= 1.5f;
                BackAcceleration *= 1.2f;
                Stabilisers *= 1.5f;
                UltAbility = true;
            }
            else if (Holder.SETTINGS.LastDifficulty == 1)
            {
                UltAbility = true;
            }
            else if (Holder.SETTINGS.LastDifficulty == 2)
            {
                Health = 75;
                Shields = 75;
                FrontAcceleration *= 0.75f;
                SideAcceleration *= 0.75f;
                BackAcceleration *= 0.75f;
                Stabilisers *= 0.5f;
                UltAbility = false;
            }
            else
            {
                Health = 50;
                Shields = 50;
                FrontAcceleration *= 0.6f;
                SideAcceleration *= 0.5f;
                BackAcceleration *= 0.6f;
                Stabilisers *= 0.3f;
                UltAbility = false;
            }
        }

        public void Update()
        {
            if (CollisionMark)
                CollisionMark = false;
            if (AskToFire)
                AskToFire = false;

            if (Holder.KSTATE.IsKeyDown(Keys.Space))
                AskToFire = true;

            //Input Down
            if (Holder.KSTATE.IsKeyDown(Keys.W))
                Velocity.Y -= FrontAcceleration;

            if (Holder.KSTATE.IsKeyDown(Keys.S))
                Velocity.Y += BackAcceleration;

            if (Holder.KSTATE.IsKeyDown(Keys.A))
            {
                Velocity.X -= SideAcceleration;
                if (Angle > -0.2f)
                    Angle -= 0.01f;
            }
            if (Holder.KSTATE.IsKeyDown(Keys.D))
            {
                Velocity.X += SideAcceleration;
                if (Angle < 0.2f)
                    Angle += 0.01f;
            }

            //Input Up
            if (Holder.KSTATE.IsKeyUp(Keys.Space))
                AskToFire = false;

            if (Holder.KSTATE.IsKeyUp(Keys.W) && Holder.KSTATE.IsKeyUp(Keys.S))
            {
                if (Velocity.Y < 0.3f)
                    Velocity.Y += Drag;
                if (Velocity.Y > -0.2f)
                    Velocity.Y -= Stabilisers;
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
            }
            UpdateByVelocity();

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

        public void PlayerHeal(int heal)
        {
            if (Health >= 100) return;
            Health += heal;
            if (Health > 100)
                Health = 100;
        }

        public void PlayerRecharge(int recharge, bool overLoad = false)
        {
            int maxShield = 100;
            if (overLoad) maxShield = 200;
            if (Shields >= maxShield) return;
            Shields += recharge;
            if (Shields > maxShield)
                Shields = maxShield;
        }
    }
}
