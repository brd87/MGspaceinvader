using Microsoft.Xna.Framework.Input;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace SpaceInvaderPlusPlus.Players
{
    public class Player
    {
        public Entity PlMain {  get; set; }
        private PlayerPart PlFront;
        private PlayerPart PlLeft;
        private PlayerPart PlRight;

        public int Health { get; set; }
        public int Shields { get; set; }

        private float FrontAcceleration;
        private float SideAcceleration;
        private float BackAcceleration;
        private float Stabilisers;
        private float Drag;

        public bool AskToFire { get; set; }
        public bool UltAbility { get; set; }

        public Player(ref General general, ref Vector2 position)
        {
            PlMain = new Entity(ref general, position, 0.0f, "player/player", 1);
            PlFront = new PlayerPart(ref general, new Vector2(general.WIDTH / 2, general.HEIGHT / 4 * 3), "player/player_front");
            PlLeft = new PlayerPart(ref general, new Vector2(general.WIDTH / 2, general.HEIGHT / 4 * 3), "player/player_lwing");
            PlRight = new PlayerPart(ref general, new Vector2(general.WIDTH / 2, general.HEIGHT / 4 * 3), "player/player_rwing");

            Health = 100;
            Shields = 100;
            FrontAcceleration = 0.6f;
            SideAcceleration = 0.4f;
            BackAcceleration = 0.4f;
            Stabilisers = 0.2f;
            Drag = 0.2f;
            if (general.SETTINGS.LastDifficulty == 0)
            {
                Health *= 2;
                Shields *= 2;
                UltAbility = true;
            }
            else if (general.SETTINGS.LastDifficulty == 1)
            {
                UltAbility = true;
            }
            else if (general.SETTINGS.LastDifficulty == 2)
            {
                Health = 75;
                Shields = 75;
                UltAbility = false;
            }
            else
            {
                Health = 50;
                Shields = 50;
                UltAbility = false;
            }
        }

        public void Update(ref General general)
        {
            if (PlMain.CollisionMark)
                PlMain.CollisionMark = false;
            if (AskToFire)
                AskToFire = false;

            if (general.KSTATE.IsKeyDown(Keys.Space))
                AskToFire = true;

            //Input Down
            if (general.KSTATE.IsKeyDown(Keys.W))
                PlMain.Velocity.Y -= FrontAcceleration;

            if (general.KSTATE.IsKeyDown(Keys.S))
                PlMain.Velocity.Y += BackAcceleration;

            if (general.KSTATE.IsKeyDown(Keys.A))
            {
                PlMain.Velocity.X -= SideAcceleration;
                if (PlMain.Angle > -0.2f)
                    PlMain.Angle -= 0.01f;
            }
            if (general.KSTATE.IsKeyDown(Keys.D))
            {
                PlMain.Velocity.X += SideAcceleration;
                if (PlMain.Angle < 0.2f)
                    PlMain.Angle += 0.01f;
            }

            //Input Up
            if (general.KSTATE.IsKeyUp(Keys.Space))
                AskToFire = false;

            if (general.KSTATE.IsKeyUp(Keys.W) && general.KSTATE.IsKeyUp(Keys.S))
            {
                if (PlMain.Velocity.Y < 0.3f)
                    PlMain.Velocity.Y += Drag;
                if (PlMain.Velocity.Y > -0.2f)
                    PlMain.Velocity.Y -= Stabilisers;
            }
            if (general.KSTATE.IsKeyUp(Keys.A) && general.KSTATE.IsKeyUp(Keys.D))
            {
                if (PlMain.Velocity.X > 0.4f)
                    PlMain.Velocity.X -= Stabilisers;
                else if (PlMain.Velocity.X < -0.4f)
                    PlMain.Velocity.X += Stabilisers;
                if (PlMain.Angle > 0.0f)
                    PlMain.Angle -= 0.01f;
                else if (PlMain.Angle < 0.0f)
                    PlMain.Angle += 0.01f;
            }
            PlMain.UpdateByVelocity();

            //COR
            if (PlMain.Position.X < 0)
            {
                PlMain.Position.X = 0;
                PlMain.Velocity.X = 0;
            }

            if (PlMain.Position.X > general.WIDTH)
            {
                PlMain.Position.X = general.WIDTH;
                PlMain.Velocity.X = 0;
            }

            if (PlMain.Position.Y < 0)
            {
                PlMain.Position.Y = 0;
                PlMain.Velocity.Y = 3;
            }

            if (PlMain.Position.Y > general.HEIGHT)
            {
                PlMain.Position.Y = general.HEIGHT;
                PlMain.Velocity.Y = 0;
            }

            PlFront.Update(ref general, ref PlMain.Position);
            PlLeft.Update(ref general, ref PlMain.Position);
            PlRight.Update(ref general, ref PlMain.Position);
        }

        public void DrawAll(ref General general)
        {
            PlFront.DrawEntity(ref general);
            PlLeft.DrawEntity(ref general);
            PlRight.DrawEntity(ref general);
            PlMain.DrawEntity(ref general);
        }

        public void PlayerDamage(ref int damage)
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

        public void PlayerHeal(ref int heal)
        {
            if (Health >= 100) return;
            Health += heal;
            if (Health > 100)
                Health = 100;
        }

        public void PlayerRecharge(ref int recharge, bool overLoad = false)
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
