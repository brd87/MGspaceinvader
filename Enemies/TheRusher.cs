using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaderPlusPlus.Enemies
{
    internal class TheRusher : Enemy
    {
        public TheRusher(Vector2 position, float angle = 0.0f, string spriteName = "therusher", int entityLayer = 1) : base(position, angle, spriteName, entityLayer)
        {
            this.Health = 2;
            this.MaxHealth = 2;
            this.Damage = 10;
            this.FrontAcceleration = 0;
            this.BackAcceleration = 0.1f;
            this.SideAcceleration = 0.8f;
            this.DamgeStageSpriteName = "therusher_dmg";
            this.ProjectileSpriteName = "therusher_bullet";
            this.AnimatedPart = new Entity(this.Position, 0.0f, "therusher_claws", 1);
            this.SelfDeathScoreCost = 100;
            this.SelfDamageScoreCost = 1;
            this.PlayerDamageScoreCost = 100;
    }

        public override void Move(Vector2 position)
        {
            if(Velocity.Y < 20)
                Velocity.Y += BackAcceleration;

            if (position.X < 0 || position.X > Holder.width)
                Velocity.X = 0;

            if(this.Position.X > position.X + 20)
            {
                if (Velocity.X > -10)
                    Velocity.X -= SideAcceleration;
                if (Angle < 0.4f)
                {
                    Angle += 0.02f;
                    AnimatedPart.Angle = 2 * Angle;
                }
                    
            }
            else if (this.Position.X < position.X - 20)
            {
                if (Velocity.X < 10)
                    Velocity.X += SideAcceleration;
                if (Angle > -0.4f)
                {
                    Angle -= 0.02f;
                    AnimatedPart.Angle = 2 * Angle;
                }
            }

            if (this.Position.X > position.X && this.Position.X < position.X + 10)
            {
                Velocity.X -= SideAcceleration*3;
                if (Angle < 0.0f)
                {
                    Angle -= 0.01f;
                    AnimatedPart.Angle = 2 * Angle;
                }
                    
            }
            else if (this.Position.X < position.X && this.Position.X > position.X - 10)
            {
                Velocity.X += SideAcceleration*3;
                if (Angle < 0.0f)
                {
                    Angle += 0.01f;
                    AnimatedPart.Angle = 2 * Angle;
                }
                    
            }

            if (position.Y - this.Position.Y < 50)
                Velocity.Y += BackAcceleration/2;

            this.Position += Velocity;
            
            this.AnimatedPart.Position = this.Position;

        }

        public override void Attack(Entity player)
        {
        }
    }
}
