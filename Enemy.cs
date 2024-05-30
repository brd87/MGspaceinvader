using Microsoft.Xna.Framework;
using SpaceInvaderPlusPlus.Players;
using System;
using System.Collections.Generic;

namespace SpaceInvaderPlusPlus
{
    public abstract class Enemy : Entity
    {
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        protected int Damage { get; set; }
        protected int SelfCollisionDamage { get; set; }
        protected int PlayerCollisionDamage { get; set; }
        protected float FrontAcceleration { get; set; }
        protected float SideAcceleration { get; set; }
        protected float BackAcceleration { get; set; }
        public Vector2 Velocity;
        protected String DamgeStageSpriteName { get; set; }
        protected String DamgeStageAnimatedPartSpriteName { get; set; }
        private bool DamgeStageCheck { get; set; }
        protected List<Entity> Projetiles { get; set; }
        protected Entity AnimatedPart { get; set; }

        protected float SelfDeathScoreCost { get; set; }
        protected float SelfDamageScoreCost { get; set; }
        protected float PlayerDamageScoreCost { get; set; }
        public bool UltRecived { get; set; }

        protected Enemy(Vector2 position, float angle, string spriteName, int entityLayer) : base(position, angle, spriteName, entityLayer)
        {
            this.Velocity = new Vector2(0.0f, 0.0f);
            DamgeStageCheck = false;
            Projetiles = new List<Entity>();
            UltRecived = false;
        }

        public void Update(Player player, Weapon weapon, GameTime gameTime = null)
        {
            if (this.CollisionMark)
                this.CollisionMark = false;

            Move(player.Position);
            CollisionCheck(player, weapon);
            Attack(player, gameTime);
            if (Health <= 0) Holder.SCORE_DMG += SelfDeathScoreCost;
        }

        private void CollisionCheck(Player player, Weapon weapon)
        {
            if (Vector2.Distance(this.Position, player.Position) < this.EntityTexture.Height / 2 + player.EntityTexture.Height / 2)
            {
                this.CollisionMark = true;
                Health -= SelfCollisionDamage;
            }
            foreach (Entity projectile in weapon.Projetiles)
            {
                if (Vector2.Distance(this.Position, projectile.Position) < this.EntityTexture.Height / 3 * 2 + projectile.EntityTexture.Width / 2)
                {
                    if (!projectile.CollisionMark || weapon.Penetration)
                    {
                        Health -= weapon.Damage;
                        projectile.CollisionMark = true;
                        Holder.SCORE_DMG += SelfDamageScoreCost;
                    }
                }
            }

            if (Health <= MaxHealth / 2)
            {
                if (!DamgeStageCheck)
                {
                    this.UpdateSprite(DamgeStageSpriteName);
                    this.AnimatedPart.UpdateSprite(DamgeStageAnimatedPartSpriteName);
                    DamgeStageCheck = true;
                }
            }
        }

        protected abstract void Move(Vector2 playerPosition);

        protected abstract void Attack(Player player, GameTime gameTime = null);

        public void DrawAll()
        {
            if (this.Projetiles != null)
            {
                foreach (var entity in Projetiles)
                {
                    entity.DrawEntity();
                }
            }

            AnimatedPart.DrawEntity();
            this.DrawEntity();
        }
    }
}