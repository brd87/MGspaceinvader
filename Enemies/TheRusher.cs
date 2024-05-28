using Microsoft.Xna.Framework;
using SpaceInvaderPlusPlus.Players;

namespace SpaceInvaderPlusPlus.Enemies
{
    internal class TheRusher : Enemy
    {
        public TheRusher(Vector2 position, float angle = 0.0f, string spriteName = "therusher", int entityLayer = 1) : base(position, angle, spriteName, entityLayer)
        {
            this.Health = 3;
            this.MaxHealth = 3;
            this.Damage = 10;
            this.SelfCollisionDamage = this.MaxHealth;
            this.PlayerCollisionDamage = 0;
            this.FrontAcceleration = 0;
            this.BackAcceleration = 0.3f;
            this.SideAcceleration = 0.8f;
            this.DamgeStageSpriteName = "therusher_dmg";
            this.DamgeStageAnimatedPartSpriteName = "therusher_claws_dmg";
            this.AnimatedPart = new Entity(this.Position, 0.0f, "therusher_claws", 1);
            this.SelfDeathScoreCost = 100;
            this.SelfDamageScoreCost = 1;
            this.PlayerDamageScoreCost = 100;
        }

        public override void Move(Vector2 playerPosition)
        {
            //Y
            if (this.Velocity.Y < 10)
                this.Velocity.Y += this.BackAcceleration;

            if (playerPosition.Y - this.Position.Y < 100)
                this.Velocity.Y += this.BackAcceleration * 2;

            //X long
            if (this.Position.X > playerPosition.X + 20)
            {
                if (this.Velocity.X > -10)
                    this.Velocity.X -= this.SideAcceleration;
                if (this.Angle < 0.4f)
                {
                    this.Angle += 0.02f;
                    this.AnimatedPart.Angle = 2 * this.Angle;
                }
            }
            else if (this.Position.X < playerPosition.X - 20)
            {
                if (this.Velocity.X < 10)
                    this.Velocity.X += this.SideAcceleration;
                if (this.Angle > -0.4f)
                {
                    this.Angle -= 0.02f;
                    this.AnimatedPart.Angle = 2 * this.Angle;
                }
            }

            //X short
            else if (this.Position.X > playerPosition.X && this.Position.X < playerPosition.X + 10)
            {
                this.Velocity.X -= SideAcceleration * 3;
                if (this.Angle < 0.0f)
                {
                    this.Angle -= 0.01f;
                    this.AnimatedPart.Angle = 2 * this.Angle;
                }
            }
            else if (this.Position.X < playerPosition.X && this.Position.X > playerPosition.X - 10)
            {
                this.Velocity.X += this.SideAcceleration * 3;
                if (this.Angle < 0.0f)
                {
                    this.Angle += 0.01f;
                    this.AnimatedPart.Angle = 2 * this.Angle;
                }
            }

            //COR
            if (this.Position.X < -100)
                this.Velocity.X += this.SideAcceleration * 2;
            else if (this.Position.X > Holder.WIDTH + 100)
                this.Velocity.X -= this.SideAcceleration * 2;


            this.Position += Velocity;
            this.AnimatedPart.Position = this.Position;

        }

        public override void Attack(Player player, GameTime gameTime = null)
        {
            if (!this.CollisionMark) return;

            player.PlayerDamage(this.Damage);
            Holder.SCORE_DMGPLAYER += this.PlayerDamageScoreCost;
            player.CollisionMark = true;
        }
    }
}
