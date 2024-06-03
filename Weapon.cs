using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace SpaceInvaderPlusPlus
{
    internal abstract class Weapon
    {
        protected Entity WepMain;
        protected float Cooldawn;
        protected TimeSpan LastTime;
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
            FireGranted = false;
            Projetiles = new List<Entity>();
        }

        public void Update(ref General general, bool AskToFire, Vector2 shipPosition, GameTime gameTime)
        {
            if (gameTime.TotalGameTime - LastTime >= TimeSpan.FromSeconds(Cooldawn))
                Loaded = true;

            if (AskToFire && Loaded && Ammunition > 0)
            {
                SoundEffectInstance WepSoundEffectIns = WepSoundEffect.CreateInstance();
                WepSoundEffectIns.Volume = general.SETTINGS.LastEffectsVolume;
                WepSoundEffectIns.Play();

                Projetiles.Add(new Entity(ref general, WepMain.Position + new Vector2(0, -10), 0.0f, ProjectileSprite, null, 0.91f));
                LastTime = gameTime.TotalGameTime;
                Loaded = false;
                Ammunition -= 1;
                FireGranted = true;
                FireEffect.Position = shipPosition - new Vector2(0, 37);
            }

            WepMain.Position = shipPosition;
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
                FireGranted = false;
            }

            WepMain.DrawEntity(ref general);
        }

        public abstract void ProjectileUpdate(Vector2 shipPosition);
    }
}
