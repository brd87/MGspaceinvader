using Microsoft.Xna.Framework;
using SpaceInvaderPlusPlus.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaderPlusPlus.Pickups
{
    internal class UltPack : Pickup
    {
        public UltPack(Vector2 position, float angle = 0.0f, string spriteName = "ult_pack", int entityLayer = 1) : base(position, angle, spriteName, entityLayer)
        {
            this.GrabScoreCost = 0;
        }

        protected override void HandleCollision(Player player, Weapon weapon)
        {
            if (player.UltAbility) return;
            else player.UltAbility = true;
        }
    }
}
