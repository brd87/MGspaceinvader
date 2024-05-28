using Microsoft.Xna.Framework;
using SpaceInvaderPlusPlus.Players;

namespace SpaceInvaderPlusPlus
{
    public abstract class Pickup : Entity
    {
        public Vector2 Velocity;
        public float Torque { get; set; }
        public float GrabScoreCost { get; set; }
        protected Pickup(Vector2 position, float angle, string spriteName, int entityLayer) : base(position, angle, spriteName, entityLayer)
        {
            Velocity = new Vector2(Holder.randomFloat(-0.2f, 0.2f), Holder.randomFloat());
            Torque = Holder.randomFloat(-0.02f, 0.02f);
        }

        public void Update(Player player, Weapon weapon)
        {
            this.Position += Velocity;
            this.Angle += Torque;
            if (this.Position.X > Holder.WIDTH || this.Position.X < 0)
            {
                Velocity.X *= -1;
                Torque *= -1;
            }

            if (Vector2.Distance(this.Position, player.Position) < this.EntityTexture.Height / 2 + player.EntityTexture.Height / 2)
            {
                this.CollisionMark = true;
                Holder.SCORE_PICKUPS += GrabScoreCost;
                HandleCollision(player, weapon);
            }
        }

        public abstract void HandleCollision(Player player, Weapon weapon);
    }
}