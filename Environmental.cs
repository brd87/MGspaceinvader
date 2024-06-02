using Microsoft.Xna.Framework;
using SpaceInvaderPlusPlus.Players;

namespace SpaceInvaderPlusPlus
{
    public abstract class Environmental
    {
        public Entity EnvMain { get; set; }
        private float Torque;
        protected int Damage;
        public bool DespawnOnHit { get; set; }
        public int ParticleSetId { get; set; }
        protected int Armor;
        protected float PlayerDamageScoreCost;
        protected float Layer;
        protected Environmental(ref General general)
        {
            Torque = general.randomFloat(-0.01f, 0.01f);
            Layer = 0.8f;
        }

        public void Update(ref General general, ref Player player, ref Weapon weapon)
        {
            this.EnvMain.UpdateByVelocity();
            this.EnvMain.Angle += Torque;
            if (this.EnvMain.Position.X > general.WIDTH || this.EnvMain.Position.X < 0)
            {
                this.EnvMain.Velocity.X *= -1;
                Torque *= -1;
            }


            if (Vector2.Distance(this.EnvMain.Position, player.PlMain.Position) < this.EnvMain.EntityTexture.Height / 2 + player.PlMain.EntityTexture.Height / 2)
            {
                HandleCollisionPlayer(ref general, ref player);
            }
            foreach (Entity projectile in weapon.Projetiles)
            {
                if (Vector2.Distance(this.EnvMain.Position, projectile.Position) < this.EnvMain.EntityTexture.Height / 3 * 2 + projectile.EntityTexture.Width / 2)
                {
                    if (!projectile.CollisionMark && weapon.Penetration >= Armor)
                        this.EnvMain.CollisionMark = true;

                    if (weapon.Penetration <= Armor)
                        projectile.CollisionMark = true;
                }
            }
        }

        public abstract void HandleCollisionPlayer(ref General general, ref Player player);
    }
}
