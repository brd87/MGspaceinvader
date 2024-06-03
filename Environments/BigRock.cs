using Microsoft.Xna.Framework;
using SpaceInvaderPlusPlus.Players;
using System;

namespace SpaceInvaderPlusPlus.Environments
{
    internal class BigRock : Environmental
    {
        public BigRock(ref General general, Vector2 position, float angle = 0.0f, string spriteName = "env/env_rock") : base(ref general)
        {
            this.EnvMain = new Entity(ref general, position, angle, general.ASSETLIBRARY.tEnv_Rock, null, this.Layer);
            this.EnvMain.Velocity = new Vector2(general.randomFloat(-0.2f, 0.2f), general.randomFloat(0.5f));
            this.Damage = 10;
            this.DespawnOnHit = false;
            this.Armor = 2;
            this.DespawnOnHit = false;
        }

        public override void HandleCollisionPlayer(ref General general, ref Player player)
        {
            Vector2 direction = this.EnvMain.Position - player.PlMain.Position;
            float distance = direction.Length();
            float combinedRadius = this.EnvMain.EntityTexture.Height / 2 + player.PlMain.EntityTexture.Height / 2;
            if (distance < combinedRadius)
            {
                player.PlMain.Velocity = Vector2.Zero;
                float penetrationDepth = combinedRadius - distance;
                Vector2 pushDirection = Vector2.Normalize(direction);
                float minPushMagnitude = 1.0f;
                float pushMagnitude = Math.Max(minPushMagnitude, penetrationDepth);
                player.PlMain.Position -= pushDirection * pushMagnitude;
            }
        }
    }
}
