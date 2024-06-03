using Microsoft.Xna.Framework;
using SpaceInvaderPlusPlus.Players;
using System;

namespace SpaceInvaderPlusPlus.Enemies
{
    internal class TheSpewer : Enemy
    {
        private float Cooldawn;
        private TimeSpan LastTime;

        public TheSpewer(ref General general, Vector2 position, float angle = 0.0f) : base(ref general)
        {
            this.EnMain = new Entity(ref general, position, angle, general.ASSETLIBRARY.tEne_Spewer, null, this.Layer);
            this.AnimatedPart = new Entity(ref general, this.EnMain.Position, 0.0f, general.ASSETLIBRARY.tEne_Spewer_Ani, null, this.Layer - 0.001f);
            this.DamgeStageSpriteName = general.ASSETLIBRARY.tEne_Spewer_Dmg;
            this.DamgeStageAnimatedPartSpriteName = general.ASSETLIBRARY.tEne_Spewer_Ani_Dmg;
            this.MaxHealth = 13;
            this.Health = this.MaxHealth;
            this.Armor = 0;
            this.ParticleSetId = 2;
            this.SelfCollisionDamage = this.MaxHealth;
            this.PlayerCollisionDamage = 80;
            this.FrontAcceleration = 0.1f;
            this.BackAcceleration = 0.1f;
            this.SideAcceleration = 0.2f;
            this.SelfDeathScoreCost = 500;
            this.SelfDamageScoreCost = 10;
            this.PlayerCollisionScoreCost = 500;

            Cooldawn = 2;
            LastTime = TimeSpan.FromSeconds(0.0f);
        }

        protected override void Move(ref General general, ref Vector2 playerPosition)
        {
            //Y
            if (this.EnMain.Velocity.Y < 1 && this.EnMain.Position.Y < 100)
                this.EnMain.Velocity.Y += this.BackAcceleration;

            else if (this.EnMain.Velocity.Y > -1 && this.EnMain.Position.Y > 250)
                this.EnMain.Velocity.Y = -this.FrontAcceleration;


            //X long
            if (this.EnMain.Position.X > playerPosition.X + 30)
            {
                if (this.EnMain.Velocity.X > -2)
                    this.EnMain.Velocity.X -= SideAcceleration;
                if (this.EnMain.Angle < 0.2f)
                {
                    this.EnMain.Angle += 0.01f;
                    this.AnimatedPart.Angle = 2 * this.EnMain.Angle;
                }
            }
            else if (this.EnMain.Position.X < playerPosition.X - 30)
            {
                if (this.EnMain.Velocity.X < 2)
                    this.EnMain.Velocity.X += this.SideAcceleration;
                if (this.EnMain.Angle > -0.2f)
                {
                    this.EnMain.Angle -= 0.01f;
                    this.AnimatedPart.Angle = 2 * this.EnMain.Angle;
                }
            }

            //X short
            else if (this.EnMain.Position.X > playerPosition.X && this.EnMain.Position.X < playerPosition.X + 10)
            {
                this.EnMain.Velocity.X -= SideAcceleration;
                if (this.EnMain.Angle < 0.0f)
                {
                    this.EnMain.Angle -= 0.01f;
                    this.AnimatedPart.Angle = 2 * this.EnMain.Angle;
                }
            }
            else if (this.EnMain.Position.X < playerPosition.X && this.EnMain.Position.X > playerPosition.X - 10)
            {
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
            if(AskTofire)
                AskTofire = false;

            if (gameTime.TotalGameTime - LastTime >= TimeSpan.FromSeconds(Cooldawn) && this.EnMain.Position.Y > 0)
            {
                AskTofire = true;
                LastTime = gameTime.TotalGameTime;
            }
        }
    }
}
