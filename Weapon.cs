using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaderPlusPlus.Utilities;
using System;
using System.Collections.Generic;

namespace SpaceInvaderPlusPlus
{
    internal abstract class Weapon
    {
        protected Entity WepMain;
        protected float Cooldawn;
        protected float BurstCooldawn;
        protected TimeSpan LastTime;
        protected TimeSpan BurstLastTime;
        protected int BurstAmount;
        private int BurstCounter;
        public int Ammunition { get; set; }
        public int MaxAmmunition { get; set; }
        public int Damage { get; set; }
        protected bool FireGranted;
        public List<Entity> Projetiles { get; set; }
        protected Entity FireEffect;
        protected Texture2D ProjectileSprite;
        protected SoundEffect WepSoundEffect;
        public int Penetration { get; set; }
        public float AmmoScoreCost { get; set; }
        public bool Loaded { get; set; }


        protected Weapon()
        {
            LastTime = TimeSpan.FromSeconds(0.0f);
            BurstLastTime = TimeSpan.FromSeconds(0.0f);
            BurstAmount = 0;
            BurstCounter = BurstAmount;
            FireGranted = false;
            Projetiles = new List<Entity>();
        }

        public void Update(ref General general, ref List<Particles> particles, bool askToFire, Vector2 shipPosition, GameTime gameTime)
        {
            if (gameTime.TotalGameTime - LastTime >= TimeSpan.FromSeconds(Cooldawn))
                Loaded = true;
            
            WepMain.Position = shipPosition;

            ParticleSpawnHandling(ref general, ref particles, gameTime);
            if (FireGranted)
                FireGranted = false;

            if (!Loaded || Ammunition <= 0)
                return;

            if (BurstAmount > 0)
            {
                if (askToFire && BurstCounter == 0)
                {
                    BurstCounter = BurstAmount;
                    BurstLastTime = gameTime.TotalGameTime;
                }

                if (BurstCounter > 0 && gameTime.TotalGameTime - BurstLastTime >= TimeSpan.FromSeconds(BurstCooldawn))
                {
                    FireProjectile(ref general, shipPosition);

                    BurstCounter--;
                    BurstLastTime = gameTime.TotalGameTime;
                    if (BurstCounter == 0)
                    {
                        LastTime = gameTime.TotalGameTime;
                        Loaded = false;
                    }
                }
            }

            else
            {
                if (askToFire)
                {
                    FireProjectile(ref general, shipPosition);
                    
                    LastTime = gameTime.TotalGameTime;
                    Loaded = false;
                }
            }

            
        }

        private void FireProjectile(ref General general, Vector2 shipPosition)
        {
            SoundEffectInstance WepSoundEffectIns = WepSoundEffect.CreateInstance();
            WepSoundEffectIns.Volume = general.SETTINGS.LastEffectsVolume;
            WepSoundEffectIns.Play();

            Projetiles.Add(new Entity(ref general, WepMain.Position + new Vector2(0, -10), 0.0f, ProjectileSprite, null, 0.91f));
            Ammunition -= 1;
            FireGranted = true;
            FireEffect.Position = shipPosition - new Vector2(0, 37);
        }

        public void DrawAll(ref General general)
        {
            if (Projetiles != null)
            {
                foreach (var entity in Projetiles)
                {
                    entity.DrawEntity(ref general);
                }
            }

            if (FireGranted)
            {
                FireEffect.DrawEntity(ref general);
                //FireGranted = false;
            }

            WepMain.DrawEntity(ref general);
        }

        public abstract void ProjectileUpdate(Vector2 shipPosition);

        public abstract void ParticleSpawnHandling(ref General general, ref List<Particles> particles, GameTime gameTime);
    }
}
