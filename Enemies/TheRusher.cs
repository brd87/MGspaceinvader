using Microsoft.Xna.Framework;
using SpaceInvaderPlusPlus.Players;

namespace SpaceInvaderPlusPlus.Enemies
{
    internal class TheRusher : Enemy
    {
        public TheRusher(ref General general, Vector2 position, float angle = 0.0f) : base(ref general)
        {
            this.EnMain = new Entity(ref general, position, angle, general.ASSETLIBRARY.tEne_Rusher, null, this.Layer);
            this.AnimatedPart = new Entity(ref general, this.EnMain.Position, 0.0f, general.ASSETLIBRARY.tEne_Rusher_Ani, null, this.Layer - 0.001f);
            this.DamgeStageSpriteName = general.ASSETLIBRARY.tEne_Rusher_Dmg;
            this.DamgeStageAnimatedPartSpriteName = general.ASSETLIBRARY.tEne_Rusher_Ani_Dmg;
            this.MaxHealth = 5;
            this.Health = this.MaxHealth;
            this.Armor = 0;
            this.Damage = 10;
            this.SelfCollisionDamage = this.MaxHealth;
            this.PlayerCollisionDamage = 0;
            this.FrontAcceleration = 0;
            this.BackAcceleration = 0.3f;
            this.SideAcceleration = 0.8f;
            this.SelfDeathScoreCost = 100;
            this.SelfDamageScoreCost = 1;
            this.PlayerDamageScoreCost = 100;
        }

        protected override void Move(ref General general, ref Vector2 playerPosition)
        {
            //Y
            if (this.EnMain.Velocity.Y < 10)
                this.EnMain.Velocity.Y += this.BackAcceleration;

            if (playerPosition.Y - this.EnMain.Position.Y < 100)
                this.EnMain.Velocity.Y += this.BackAcceleration * 2;

            //X long
            if (this.EnMain.Position.X > playerPosition.X + 20)
            {
                if (this.EnMain.Velocity.X > -10)
                    this.EnMain.Velocity.X -= this.SideAcceleration;
                if (this.EnMain.Angle < 0.4f)
                {
                    this.EnMain.Angle += 0.02f;
                    this.AnimatedPart.Angle = 2 * this.EnMain.Angle;
                }
            }
            else if (this.EnMain.Position.X < playerPosition.X - 20)
            {
                if (this.EnMain.Velocity.X < 10)
                    this.EnMain.Velocity.X += this.SideAcceleration;
                if (this.EnMain.Angle > -0.4f)
                {
                    this.EnMain.Angle -= 0.02f;
                    this.AnimatedPart.Angle = 2 * this.EnMain.Angle;
                }
            }

            //X short
            else if (this.EnMain.Position.X > playerPosition.X && this.EnMain.Position.X < playerPosition.X + 10)
            {
                this.EnMain.Velocity.X -= SideAcceleration * 3;
                if (this.EnMain.Angle < 0.0f)
                {
                    this.EnMain.Angle -= 0.01f;
                    this.AnimatedPart.Angle = 2 * this.EnMain.Angle;
                }
            }
            else if (this.EnMain.Position.X < playerPosition.X && this.EnMain.Position.X > playerPosition.X - 10)
            {
                this.EnMain.Velocity.X += this.SideAcceleration * 3;
                if (this.EnMain.Angle < 0.0f)
                {
                    this.EnMain.Angle += 0.01f;
                    this.AnimatedPart.Angle = 2 * this.EnMain.Angle;
                }
            }

            //COR
            if (this.EnMain.Position.X < -100)
                this.EnMain.Velocity.X += this.SideAcceleration * 2;
            else if (this.EnMain.Position.X > general.WIDTH + 100)
                this.EnMain.Velocity.X -= this.SideAcceleration * 2;
        }

        protected override void Attack(ref General general, ref Player player, GameTime gameTime = null)
        {
            if (!this.EnMain.CollisionMark) return;

            int damage = this.Damage;
            player.PlayerDamage(ref damage);
            general.SCORE_DMGPLAYER += this.PlayerDamageScoreCost;
        }
    }
}
