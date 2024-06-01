using Microsoft.Xna.Framework;
using SpaceInvaderPlusPlus.Players;

namespace SpaceInvaderPlusPlus.Environments
{
    internal class AcidMine : Environmental
    {
        public AcidMine(Vector2 position, float angle = 0.0f, string spriteName = "env/env_mine") : base(position, angle, spriteName)
        {
            this.Damage = 50;
            this.DespawnOnHit = true;
            this.Armor = 0;
            this.PlayerDamageScoreCost = 100;
        }

        public override void HandleCollisionPlayer(Player player)
        {
            player.PlayerDamage(this.Damage);
            this.CollisionMark = true;
            Holder.SCORE_DMGPLAYER += this.PlayerDamageScoreCost;
            player.CollisionMark = true;
        }
    }
}
