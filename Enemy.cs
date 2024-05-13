using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaderPlusPlus
{
    public abstract class Enemy : Entity
    {
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Damage { get; set; }
        public float FrontAcceleration { get; set; }
        public float SideAcceleration { get; set; }
        public float BackAcceleration { get; set; }
        public Vector2 Velocity;
        public String DamgeStageSpriteName {  get; set; }
        public bool DamgeStageCheck { get; set; }
        public List<Entity> Projetiles { get; set; }
        public string ProjectileSpriteName { get; set; }
        public Entity FireEffect { get; set; }
        public Entity AnimatedPart {  get; set; }

        public float SelfDeathScoreCost { get; set; }
        public float SelfDamageScoreCost { get; set; }
        public float PlayerDamageScoreCost { get; set; }

        protected Enemy(Vector2 position, float angle, string spriteName, int entityLayer) : base(position, angle, spriteName, entityLayer)
        {
            this.Velocity = new Vector2(0.0f, 0.0f);
            DamgeStageCheck = false;
            Projetiles = new List<Entity>();
        }

        public void Update(Vector2 position, Entity player, List<Entity> weaponProjectiles, int weaponDamage, GameTime gameTime = null)
        {
            if(this.CollisionMark)
                this.CollisionMark = false;
            foreach(Entity projectile in Projetiles)
                if(projectile.CollisionMark)
                    projectile.CollisionMark = false;

            //CollisionCheck(player, weaponProjectiles, weaponDamage);
            Move(position);
            Attack(player);
            CollisionCheck(player, weaponProjectiles, weaponDamage);
            if (Health <= 0) Holder.score += SelfDeathScoreCost;
        }

        public void CollisionCheck(Entity entity, List<Entity> weaponProjectiles, int weaponDamage)
        {
            Rectangle body = new Rectangle((int)this.Position.X, (int)this.Position.Y, this.EntityTexture.Width, this.EntityTexture.Height);
            if (body.Intersects(new Rectangle((int)entity.Position.X, (int)entity.Position.Y, entity.EntityTexture.Width, entity.EntityTexture.Height)))
            {
                Health -= 5;
                this.CollisionMark = true;
                Holder.score =- PlayerDamageScoreCost;
            }
            foreach (Entity projectile in weaponProjectiles)
            {
                if(body.Intersects(new Rectangle((int)projectile.Position.X, (int)projectile.Position.Y, projectile.EntityTexture.Width, projectile.EntityTexture.Height)))
                {
                    Health -= weaponDamage;
                    projectile.CollisionMark = true;
                    Holder.score += SelfDamageScoreCost;
                }
            }

            if (Health <= MaxHealth / 2)
            {
                if (!DamgeStageCheck)
                {
                    this.UpdateSprite(DamgeStageSpriteName);
                    DamgeStageCheck = true;
                }
            }
        }

        public abstract void Move(Vector2 position);

        public abstract void Attack(Entity player);
        
        public void DrawAll()
        {
            AnimatedPart.DrawEntity();
            this.DrawEntity();
        }
    }
}