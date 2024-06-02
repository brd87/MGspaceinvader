using Microsoft.Xna.Framework;
using SpaceInvaderPlusPlus.Players;

namespace SpaceInvaderPlusPlus.Enemies
{
    internal class TheWall : Enemy
    {
        public TheWall(ref General general, Vector2 position, float angle = 0.0f, string spriteName = "ene/ene_thewall") : base(ref general)
        {
            this.EnMain = new Entity(ref general, position, angle, spriteName, this.Layer);
            this.MaxHealth = 20;
            this.Health = this.MaxHealth;
            this.Armor = 1;
            this.Damage = 10;
            this.SelfCollisionDamage = 0;
            this.PlayerCollisionDamage = 1;
            this.FrontAcceleration = 1f;
            this.BackAcceleration = 0.1f;
            this.SideAcceleration = 0.4f;
            this.DamgeStageSpriteName = "ene/ene_thewall_dmg";
            this.DamgeStageAnimatedPartSpriteName = "ene/ene_thewall_wings_dmg";
            this.AnimatedPart = new Entity(ref general, this.EnMain.Position, 0.0f, "ene/ene_thewall_wings", null, this.Layer);
            this.SelfDeathScoreCost = 500;
            this.SelfDamageScoreCost = 10;
            this.PlayerDamageScoreCost = 2;
        }

        protected override void Move(ref General general, ref Vector2 playerPosition)
        {
            //Y
            if (this.EnMain.Velocity.Y < 2 && playerPosition.Y - this.EnMain.Position.Y > 450)
                this.EnMain.Velocity.Y += this.BackAcceleration;

            else if (this.EnMain.Velocity.Y > -8 && playerPosition.Y - this.EnMain.Position.Y <= 400 && this.EnMain.Position.Y > 100)
                this.EnMain.Velocity.Y = -this.FrontAcceleration;

            //X long
            if (this.EnMain.Position.X > playerPosition.X + 20)
            {
                if (this.EnMain.Velocity.X > -5)
                    this.EnMain.Velocity.X -= this.SideAcceleration;
                if (this.EnMain.Angle < 0.2f)
                {
                    this.EnMain.Angle += 0.01f;
                    this.AnimatedPart.Angle = 3 * this.EnMain.Angle;
                }

            }
            else if (this.EnMain.Position.X < playerPosition.X - 20)
            {
                if (this.EnMain.Velocity.X < 5)
                    this.EnMain.Velocity.X += this.SideAcceleration;
                if (this.EnMain.Angle > -0.2f)
                {
                    this.EnMain.Angle -= 0.01f;
                    this.AnimatedPart.Angle = 3 * this.EnMain.Angle;
                }
            }

            //X short
            else if (this.EnMain.Position.X > playerPosition.X && this.EnMain.Position.X <= playerPosition.X + 20)
            {
                if (this.EnMain.Velocity.X > 0)
                    this.EnMain.Velocity.X -= this.SideAcceleration;

                if (this.EnMain.Angle < 0.0f)
                {
                    this.EnMain.Angle -= 0.01f;
                    this.AnimatedPart.Angle = 2 * this.EnMain.Angle;
                }
            }
            else if (this.EnMain.Position.X < playerPosition.X && this.EnMain.Position.X >= playerPosition.X - 20)
            {
                if (this.EnMain.Velocity.X < 0)
                    this.EnMain.Velocity.X += this.SideAcceleration;

                if (this.EnMain.Angle < 0.0f)
                {
                    this.EnMain.Angle += 0.01f;
                    this.AnimatedPart.Angle = 2 * this.EnMain.Angle;
                }
            }

            //COR
            if (this.EnMain.Position.X < 0)
                this.EnMain.Velocity.X += this.SideAcceleration * 3;
            else if (this.EnMain.Position.X > general.WIDTH)
                this.EnMain.Velocity.X -= this.SideAcceleration * 3;
        }

        protected override void Attack(ref General general, ref Player player, GameTime gameTime = null)
        {
            Rectangle body = new Rectangle((int)this.EnMain.Position.X, (int)this.EnMain.Position.Y, this.EnMain.EntityTexture.Width, this.EnMain.EntityTexture.Height);
            if (body.Intersects(new Rectangle((int)player.PlMain.Position.X, (int)player.PlMain.Position.Y, player.PlMain.EntityTexture.Width, player.PlMain.EntityTexture.Height)))
            {
                if (player.PlMain.Position.Y > this.EnMain.Position.Y)
                    if (player.PlMain.Velocity.Y < this.EnMain.Velocity.Y)
                    {
                        player.PlMain.Velocity.Y = this.EnMain.Velocity.Y * 1.1f;
                    }
            }
        }
    }
}
