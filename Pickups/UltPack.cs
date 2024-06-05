using Microsoft.Xna.Framework;
using SpaceInvaderPlusPlus.Players;

namespace SpaceInvaderPlusPlus.Pickups
{
    internal class UltPack : Pickup
    {
        private int Reacharge;
        public UltPack(ref General general, Vector2 position, float angle = 0.0f) : base(ref general)
        {
            this.PicMain = new Entity(ref general, position, angle, general.ASSETLIBRARY.tPack_UltAbility, null, this.Layer);
            this.PicMain.Velocity = new Vector2(general.randomFloat(-0.2f, 0.2f), general.randomFloat(0.5f));
            this.GrabScoreCost = 0;
            Reacharge = 50;
        }

        protected override void HandleCollision(ref General general, ref Player player, ref Weapon weapon)
        {
            player.PlayerRecharge(ref Reacharge);
            if (player.UltAbility) return;
            else player.UltAbility = true;
        }
    }
}
