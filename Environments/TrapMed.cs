using Microsoft.Xna.Framework;
using SpaceInvaderPlusPlus.Players;

namespace SpaceInvaderPlusPlus.Environments
{
    internal class TrapMed : Environmental
    {
        public TrapMed(Vector2 position, float angle = 0.0f, string spriteName = "env/env_med", int entityLayer = 1) : base(position, angle, spriteName, entityLayer)
        {
            this.Damage = 20;
            this.DespawnOnHit = true;
            this.PlayerDamageScoreCost = 50;
        }

        public override void HandleCollisionPlayer(Player player)
        {
            player.PlayerDamage(this.Damage);
            player.Velocity /= 4; //new Vector2(-0.5f, -0.5f);
            this.CollisionMark = true;
            Holder.SCORE_DMGPLAYER += this.PlayerDamageScoreCost;
            player.CollisionMark = true;
        }

        public override void HandleCollisionProjectile(Entity projetile)
        {
            projetile.CollisionMark = true;
            this.CollisionMark = true;
        }
    }
}
