using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using SpaceInvaderPlusPlus.Players;
using System;
using System.Collections.Generic;

namespace SpaceInvaderPlusPlus
{
    public abstract class Enemy : Entity
    {
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Armor { get; set; }
        protected int Damage { get; set; }
        protected int SelfCollisionDamage { get; set; }
        protected int PlayerCollisionDamage { get; set; }
        protected float FrontAcceleration { get; set; }
        protected float SideAcceleration { get; set; }
        protected float BackAcceleration { get; set; }
        protected String DamgeStageSpriteName { get; set; }
        protected String DamgeStageAnimatedPartSpriteName { get; set; }
        private bool DamgeStageCheck { get; set; }
        protected List<Entity> Projetiles { get; set; }
        protected Entity AnimatedPart { get; set; }
        private SoundEffectInstance DeathSoundEffectIns { get; set; }
        protected float SelfDeathScoreCost { get; set; }
        protected float SelfDamageScoreCost { get; set; }
        protected float PlayerDamageScoreCost { get; set; }
        public bool UltRecived { get; set; }

        protected Enemy(Vector2 position, float angle, string spriteName) : base(position, angle, spriteName)
        {
            DamgeStageCheck = false;
            Projetiles = new List<Entity>();
            UltRecived = false;
            DeathSoundEffectIns = Holder.CONTENT.Load<SoundEffect>("eff/eff_death").CreateInstance();
            DeathSoundEffectIns.Volume = Holder.SETTINGS.LastEffectsVolume;
        }

        public void Update(Player player, Weapon weapon, GameTime gameTime = null)
        {
            if (this.CollisionMark)
                this.CollisionMark = false;

            Move(player.Position);
            UpdateByVelocity();
            this.AnimatedPart.Position = this.Position;
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
                if (Vector2.Distance(this.Position, projectile.Position) < this.EntityTexture.Height / 3 * 2 + projectile.EntityTexture.Height / 3)
                {
                    if (!projectile.CollisionMark || weapon.Penetration >= Armor)
                    {
                        Health -= weapon.Damage;
                        Holder.SCORE_DMG += SelfDamageScoreCost;
                    }

                    if (weapon.Penetration <= Armor)
                        projectile.CollisionMark = true;
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

                if (Health <= 0)
                    DeathSoundEffectIns.Play();
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