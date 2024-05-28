using Microsoft.Xna.Framework;
using SpaceInvaderPlusPlus.Players;

namespace SpaceInvaderPlusPlus.Pickups
{
    public class MedPack : Pickup
    {
        public MedPack(Vector2 position, float angle = 0.0f, string spriteName = "med_pack", int entityLayer = 1) : base(position, angle, spriteName, entityLayer)
        {
            this.GrabScoreCost = 500;
        }

        public override void HandleCollision(Player player, Weapon weapon)
        {
            if (player.Health >= 100)
                return;

            if (Holder.SETTINGS.LastDifficulty == 0 || Holder.SETTINGS.LastDifficulty == 1)
            {
                player.Health = 100;
                return;
            }
            else if (Holder.SETTINGS.LastDifficulty == 2)
                player.Health += 50;
            else
                player.Health += 25;

            if (player.Health > 100)
                player.Health = 100;
        }
    }
}
