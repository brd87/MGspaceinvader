using Microsoft.Xna.Framework;
using SpaceInvaderPlusPlus.Players;

namespace SpaceInvaderPlusPlus.Pickups
{
    internal class UltPack : Pickup
    {
        public UltPack(Vector2 position, float angle = 0.0f, string spriteName = "pack/pack_ult") : base(position, angle, spriteName)
        {
            this.GrabScoreCost = 0;
        }

        protected override void HandleCollision(Player player, Weapon weapon)
        {
            player.PlayerRecharge(50);
            if (player.UltAbility) return;
            else player.UltAbility = true;
        }
    }
}
