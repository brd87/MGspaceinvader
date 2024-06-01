using Microsoft.Xna.Framework;
using SpaceInvaderPlusPlus.Players;

namespace SpaceInvaderPlusPlus.Environments
{
    internal class BigRock : Environmental
    {
        public BigRock(Vector2 position, float angle = 0.0f, string spriteName = "env/env_rock", int entityLayer = 1) : base(position, angle, spriteName, entityLayer)
        {
            this.Damage = 10;
            this.Armor = 2;
            this.DespawnOnHit = false;
        }

        public override void HandleCollisionPlayer(Player player)
        {
            //player.Velocity /= 2;
            if (player.Velocity.Y < this.Velocity.Y)
            {
                player.Velocity.Y = this.Velocity.Y * 1.1f;
            }
        }
    }
}
