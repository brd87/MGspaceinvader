using Microsoft.Xna.Framework;
using SpaceInvaderPlusPlus.Players;

namespace SpaceInvaderPlusPlus.Pickups
{
    public class MedPack : Pickup
    {
        public MedPack(Vector2 position, float angle = 0.0f, string spriteName = "pack/pack_med") : base(position, angle, spriteName)
        {
            this.GrabScoreCost = 500;
        }

        protected override void HandleCollision(Player player, Weapon weapon)
        {
            if (Holder.SETTINGS.LastDifficulty == 0 || Holder.SETTINGS.LastDifficulty == 1)
            {
                player.PlayerHeal(100);
            }
            else
            {
                player.PlayerHeal(50);
            }
        }
    }
}
