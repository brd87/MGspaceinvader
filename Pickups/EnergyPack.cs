using Microsoft.Xna.Framework;
using SpaceInvaderPlusPlus.Players;

namespace SpaceInvaderPlusPlus.Pickups
{
    internal class EnergyPack : Pickup
    {
        public EnergyPack(Vector2 position, float angle = 0.0f, string spriteName = "pack/pack_energy") : base(position, angle, spriteName)
        {
            this.GrabScoreCost = 50;
        }

        protected override void HandleCollision(Player player, Weapon weapon)
        {
            if (weapon.Ammunition < weapon.MaxAmmunition)
            {
                if (Holder.SETTINGS.LastDifficulty == 0)
                {
                    weapon.Ammunition = weapon.MaxAmmunition;
                    return;
                }
                else if (Holder.SETTINGS.LastDifficulty == 1)
                    weapon.Ammunition += weapon.MaxAmmunition / 2;
                else
                    weapon.Ammunition += weapon.MaxAmmunition / 4;

                if (weapon.Ammunition > weapon.MaxAmmunition)
                    weapon.Ammunition = weapon.MaxAmmunition;
            }
            else
                player.PlayerRecharge(200, true);
        }
    }
}
