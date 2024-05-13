using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaderPlusPlus
{
    public abstract class Weapon : Entity
    {
        public float Cooldawn { get; set; }
        public TimeSpan LastTime { get; set; }
        public int Ammunition { get; set; }
        public int MaxAmmunition { get; set; }
        public int Damage { get; set; }
        public bool FireGranted { get; set; }
        public List<Entity> Projetiles { get; set; }
        public Entity FireEffect { get; set; }
        public string ProjectileSpriteName { get; set; }
        public bool Penetration {  get; set; }
        public float AmmoScoreCost { get; set; }

        protected Weapon(Microsoft.Xna.Framework.Vector2 position, float angle, string spriteName, int entityLayer) : base(position, angle, spriteName, entityLayer)
        {

            this.LastTime = TimeSpan.FromSeconds(0.0f);
            this.FireGranted = false;
            this.Projetiles = new List<Entity> { };
        }

        public void Update(bool AskToFire, Microsoft.Xna.Framework.Vector2 shipPosition, GameTime gameTime)
        {
            
            if (AskToFire && (gameTime.TotalGameTime - LastTime >= TimeSpan.FromSeconds(Cooldawn)) && Ammunition > 0)
            {
                Projetiles.Add(new Entity(this.Position + new Microsoft.Xna.Framework.Vector2(0, 25), 0.0f, ProjectileSpriteName, 1));
                LastTime = gameTime.TotalGameTime;
                Ammunition -= 1;
                FireGranted = true;
                Holder.score -= AmmoScoreCost;
                FireEffect.Position = shipPosition - new Microsoft.Xna.Framework.Vector2(0, 37);
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

        public abstract void ProjectileUpdate();

        public abstract void ProjectileHitCheck();
    }
}
