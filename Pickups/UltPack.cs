using Microsoft.Xna.Framework;
using SpaceInvaderPlusPlus.Players;

namespace SpaceInvaderPlusPlus.Pickups
{
    internal class UltPack : Pickup
    {
        private int Reacharge;
        public UltPack(ref General general, Vector2 position, float angle = 0.0f, string spriteName = "pack/pack_ult") : base(ref general)
        {
            PicMain = new Entity(ref general, position, angle, spriteName, this.Layer);
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
