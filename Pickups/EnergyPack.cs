using Microsoft.Xna.Framework;
using SpaceInvaderPlusPlus.Players;

namespace SpaceInvaderPlusPlus.Pickups
{
    internal class EnergyPack : Pickup
    {
        private int Reacharge;
        public EnergyPack(ref General general, Vector2 position, float angle = 0.0f) : base(ref general)
        {
            this.PicMain = new Entity(ref general, position, angle, general.ASSETLIBRARY.tPack_Energy, null, this.Layer);
            this.PicMain.Velocity = new Vector2(general.randomFloat(-0.2f, 0.2f), general.randomFloat(0.5f));
            this.GrabScoreCost = 50;
            Reacharge = 200;
        }

        protected override void HandleCollision(ref General general, ref Player player, ref Weapon weapon)
        {
            if (weapon.Ammunition < weapon.MaxAmmunition)
            {
                if (general.SETTINGS.LastDifficulty == 0)
                {
                    weapon.Ammunition = weapon.MaxAmmunition;
                    return;
                }
                else if (general.SETTINGS.LastDifficulty == 1)
                    weapon.Ammunition += weapon.MaxAmmunition / 2;
                else
                    weapon.Ammunition += weapon.MaxAmmunition / 4;

                if (weapon.Ammunition > weapon.MaxAmmunition)
                    weapon.Ammunition = weapon.MaxAmmunition;
            }
            else
                player.PlayerRecharge(ref Reacharge, true);
        }
    }
}
