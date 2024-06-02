﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using SpaceInvaderPlusPlus.Players;
using System;
using System.Collections.Generic;

namespace SpaceInvaderPlusPlus
{
    public abstract class Enemy
    {
        public Entity EnMain {  get; set; }
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
        protected float Layer;

        protected Enemy(ref General general)
        {
            DamgeStageCheck = false;
            Projetiles = new List<Entity>();
            UltRecived = false;
            DeathSoundEffectIns = general.CONTENT.Load<SoundEffect>("eff/eff_death").CreateInstance();
            DeathSoundEffectIns.Volume = general.SETTINGS.LastEffectsVolume;
            Layer = 0.9f;
        }

        public void Update(ref General general, ref Player player, ref Weapon weapon, GameTime gameTime = null)
        {
            if (this.EnMain.CollisionMark)
                this.EnMain.CollisionMark = false;

            Move(ref general, ref player.PlMain.Position);
            EnMain.UpdateByVelocity();
            this.AnimatedPart.Position = this.EnMain.Position;
            CollisionCheck(ref general, ref player, ref weapon);
            Attack(ref general, ref player, gameTime);
            if (Health <= 0) general.SCORE_DMG += SelfDeathScoreCost;
        }

        private void CollisionCheck(ref General general, ref Player player, ref Weapon weapon)
        {
            if (Vector2.Distance(this.EnMain.Position, player.PlMain.Position) < this.EnMain.EntityTexture.Height / 2 + player.PlMain.EntityTexture.Height / 2)
            {
                this.EnMain.CollisionMark = true;
                Health -= SelfCollisionDamage;
            }
            foreach (Entity projectile in weapon.Projetiles)
            {
                if (Vector2.Distance(this.EnMain.Position, projectile.Position) < this.EnMain.EntityTexture.Height / 3 * 2 + projectile.EntityTexture.Height / 3)
                {
                    if (!projectile.CollisionMark || weapon.Penetration >= Armor)
                    {
                        Health -= weapon.Damage;
                        general.SCORE_DMG += SelfDamageScoreCost;
                    }

                    if (weapon.Penetration <= Armor)
                        projectile.CollisionMark = true;
                }
            }

            if (Health <= MaxHealth / 2)
            {
                if (!DamgeStageCheck)
                {
                    this.EnMain.UpdateSprite(ref general, DamgeStageSpriteName);
                    this.AnimatedPart.UpdateSprite(ref general,  DamgeStageAnimatedPartSpriteName);
                    DamgeStageCheck = true;
                }

                if (Health <= 0)
                    DeathSoundEffectIns.Play();
            }
        }

        protected abstract void Move(ref General general, ref Vector2 playerPosition);

        protected abstract void Attack(ref General general, ref Player player, GameTime gameTime = null);

        public void DrawAll(ref General general)
        {
            if (this.Projetiles != null)
            {
                foreach (var entity in Projetiles)
                {
                    entity.DrawEntity(ref general);
                }
            }

            AnimatedPart.DrawEntity(ref general);
            this.EnMain.DrawEntity(ref general);
        }
    }
}