using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace SpaceInvaderPlusPlus
{
    public abstract class Weapon : Entity
    {
        protected float Cooldawn { get; set; }
        protected TimeSpan LastTime { get; set; }
        public int Ammunition { get; set; }
        public int MaxAmmunition { get; set; }
        public int Damage { get; set; }
        protected bool FireGranted { get; set; }
        public List<Entity> Projetiles { get; set; }
        protected Entity FireEffect { get; set; }
        protected string ProjectileSpriteName { get; set; }
        public bool Penetration { get; set; }
        public float AmmoScoreCost { get; set; }
        public bool Loaded { get; set; }

        protected Weapon(Vector2 position, float angle, string spriteName, int entityLayer) : base(position, angle, spriteName, entityLayer)
        {

            this.LastTime = TimeSpan.FromSeconds(0.0f);
            this.FireGranted = false;
            this.Projetiles = new List<Entity> { };
        }

        public void Update(bool AskToFire, Vector2 shipPosition, GameTime gameTime)
        {
            if (gameTime.TotalGameTime - LastTime >= TimeSpan.FromSeconds(Cooldawn))
                Loaded = true;

            if (AskToFire && Loaded && Ammunition > 0)
            {
                Projetiles.Add(new Entity(this.Position + new Vector2(0, -10), 0.0f, ProjectileSpriteName, 1));
                LastTime = gameTime.TotalGameTime;
                Loaded = false;
                Ammunition -= 1;
                FireGranted = true;
                FireEffect.Position = shipPosition - new Vector2(0, 37);
            }

            this.Position = shipPosition;
        }

        public void DrawAll()
        {
            if (this.Projetiles != null)
            {
                foreach (var entity in Projetiles)
                {
                    entity.DrawEntity();
                }
            }

            if (FireGranted)
            {
                FireEffect.DrawEntity();
                FireGranted = false;
            }

            this.DrawEntity();
        }

        public abstract void ProjectileUpdate(Vector2 shipPosition);
    }
}
