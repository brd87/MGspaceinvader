using Microsoft.Xna.Framework;
using SpaceInvaderPlusPlus.Players;

namespace SpaceInvaderPlusPlus.Enemies
{
    internal class TheWall : Enemy
    {
        public TheWall(Vector2 position, float angle = 0.0f, string spriteName = "thewall", int entityLayer = 1) : base(position, angle, spriteName, entityLayer)
        {
            this.Health = 20;
            this.MaxHealth = 20;
            this.Damage = 10;
            this.SelfCollisionDamage = 0;
            this.PlayerCollisionDamage = 1;
            this.FrontAcceleration = 1f;
            this.BackAcceleration = 0.1f;
            this.SideAcceleration = 0.4f;
            this.DamgeStageSpriteName = "thewall_dmg";
            this.DamgeStageAnimatedPartSpriteName = "thewall_wings_dmg";
            this.AnimatedPart = new Entity(this.Position, 0.0f, "thewall_wings", 1);
            this.SelfDeathScoreCost = 500;
            this.SelfDamageScoreCost = 10;
            this.PlayerDamageScoreCost = 2;
        }

        protected override void Move(Vector2 playerPosition)
        {
            //Y
            if (this.Velocity.Y < 2 && playerPosition.Y - this.Position.Y > 450)
                this.Velocity.Y += this.BackAcceleration;

            else if (this.Velocity.Y > -8 && playerPosition.Y - this.Position.Y <= 400 && this.Position.Y > 100)
                this.Velocity.Y = -this.FrontAcceleration;

            //X long
            if (this.Position.X > playerPosition.X + 20)
            {
                if (this.Velocity.X > -5)
                    this.Velocity.X -= this.SideAcceleration;
                if (this.Angle < 0.2f)
                {
                    this.Angle += 0.01f;
                    this.AnimatedPart.Angle = 3 * this.Angle;
                }

            }
            else if (this.Position.X < playerPosition.X - 20)
            {
                if (this.Velocity.X < 5)
                    this.Velocity.X += this.SideAcceleration;
                if (this.Angle > -0.2f)
                {
                    this.Angle -= 0.01f;
                    this.AnimatedPart.Angle = 3 * this.Angle;
                }
            }

            //X short
            else if (this.Position.X > playerPosition.X && this.Position.X <= playerPosition.X + 20)
            {
                if (this.Velocity.X > 0)
                    this.Velocity.X -= this.SideAcceleration;

                if (this.Angle < 0.0f)
                {
                    this.Angle -= 0.01f;
                    this.AnimatedPart.Angle = 2 * this.Angle;
                }
            }
            else if (this.Position.X < playerPosition.X && this.Position.X >= playerPosition.X - 20)
            {
                if (this.Velocity.X < 0)
                    this.Velocity.X += this.SideAcceleration;

                if (this.Angle < 0.0f)
                {
                    this.Angle += 0.01f;
                    this.AnimatedPart.Angle = 2 * Angle;
                }
            }

            //COR
            if (this.Position.X < 0)
                this.Velocity.X += this.SideAcceleration * 3;
            else if (this.Position.X > Holder.WIDTH)
                this.Velocity.X -= this.SideAcceleration * 3;
        }

        protected override void Attack(Player player, GameTime gameTime = null)
        {
            Rectangle body = new Rectangle((int)this.Position.X, (int)this.Position.Y, this.EntityTexture.Width, this.EntityTexture.Height);
            if (body.Intersects(new Rectangle((int)player.Position.X, (int)player.Position.Y, player.EntityTexture.Width, player.EntityTexture.Height)))
            {
                if (player.Position.Y > this.Position.Y)
                    if (player.Velocity.Y < this.Velocity.Y)
                    {
                        player.Velocity.Y = this.Velocity.Y * 1.1f;
                    }
            }
        }
    }
}
