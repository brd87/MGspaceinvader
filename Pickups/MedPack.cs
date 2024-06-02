using Microsoft.Xna.Framework;
using SpaceInvaderPlusPlus.Players;

namespace SpaceInvaderPlusPlus.Pickups
{
    public class MedPack : Pickup
    {
        private int Heal;
        public MedPack(ref General general, Vector2 position, float angle = 0.0f, string spriteName = "pack/pack_med") : base(ref general)
        {
            PicMain = new Entity(ref general, position, angle, spriteName, this.Layer);
            this.GrabScoreCost = 500;
            if (general.SETTINGS.LastDifficulty == 0 || general.SETTINGS.LastDifficulty == 1)
                Heal = 100;
            else
                Heal = 50;
        }

        protected override void HandleCollision(ref General general, ref Player player, ref Weapon weapon)
        {
            player.PlayerHeal(ref Heal);
        }
    }
}
