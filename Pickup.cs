using Microsoft.Xna.Framework;
using SpaceInvaderPlusPlus.Players;

namespace SpaceInvaderPlusPlus
{
    internal abstract class Pickup
    {
        public Entity PicMain { get; set; }
        private float Torque;
        protected float GrabScoreCost;
        protected float Layer;
        protected Pickup(ref General general)
        {
            Torque = general.randomFloat(-0.02f, 0.02f);
            Layer = 0.6f;
        }

        public void Update(ref General general, ref Player player, ref Weapon weapon)
        {
            this.PicMain.UpdateByVelocity();
            this.PicMain.Angle += Torque;
            if (this.PicMain.Position.X > general.WIDTH || this.PicMain.Position.X < 0)
            {
                this.PicMain.Velocity.X *= -1;
                Torque *= -1;
            }

            if (Vector2.Distance(this.PicMain.Position, player.PlMain.Position) < this.PicMain.EntityTexture.Height / 2 + player.PlMain.EntityTexture.Height / 2)
            {
                this.PicMain.CollisionMark = true;
                general.SCORE_PICKUPS += GrabScoreCost;
                HandleCollision(ref general, ref player, ref weapon);
            }
        }

        protected abstract void HandleCollision(ref General general, ref Player player, ref Weapon weapon);
    }
}