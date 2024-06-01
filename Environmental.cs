﻿using Microsoft.Xna.Framework;
using SpaceInvaderPlusPlus.Players;

namespace SpaceInvaderPlusPlus
{
    public abstract class Environmental : Entity
    {
        private float Torque;
        protected int Damage;
        public bool DespawnOnHit { get; set; }
        protected int Armor;
        protected float PlayerDamageScoreCost;
        protected Environmental(Vector2 position, float angle, string spriteName, int entityLayer) : base(position, angle, spriteName, entityLayer)
        {
            Velocity = new Vector2(Holder.randomFloat(-0.2f, 0.2f), Holder.randomFloat(0.5f));
            Torque = Holder.randomFloat(-0.01f, 0.01f);
        }

        public void Update(Player player, Weapon weapon)
        {
            this.UpdateByVelocity();
            this.Angle += Torque;
            if (this.Position.X > Holder.WIDTH || this.Position.X < 0)
            {
                Velocity.X *= -1;
                Torque *= -1;
            }


            if (Vector2.Distance(this.Position, player.Position) < this.EntityTexture.Height / 2 + player.EntityTexture.Height / 2)
            {
                HandleCollisionPlayer(player);
            }
            foreach (Entity projectile in weapon.Projetiles)
            {
                if (Vector2.Distance(this.Position, projectile.Position) < this.EntityTexture.Height / 3 * 2 + projectile.EntityTexture.Width / 2)
                {
                    if (!projectile.CollisionMark && weapon.Penetration >= Armor)
                        this.CollisionMark = true;

                    if (weapon.Penetration <= Armor)
                        projectile.CollisionMark = true;
                }
            }
        }

        public abstract void HandleCollisionPlayer(Player player);
    }
}
