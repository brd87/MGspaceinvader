using Microsoft.Xna.Framework;
using SpaceInvaderPlusPlus.Players;

namespace SpaceInvaderPlusPlus.Environments
{
    internal class TrapMed : Environmental
    {
        public TrapMed(ref General general, Vector2 position, float angle = 0.0f, string spriteName = "env/env_med") : base(ref general)
        {
            this.EnvMain = new Entity(ref general, position, angle, spriteName, this.Layer);
            this.EnvMain.Velocity = new Vector2(general.randomFloat(-0.2f, 0.2f), general.randomFloat(0.5f));
            this.Damage = 20;
            this.DespawnOnHit = true;
            this.ParticleSetId = 4;
            this.Armor = 1;
            this.PlayerDamageScoreCost = 50;
        }

        public override void HandleCollisionPlayer(ref General general, ref Player player)
        {
            player.PlayerDamage(ref this.Damage);
            player.PlMain.Velocity /= 4;
            this.EnvMain.CollisionMark = true;
            general.SCORE_DMGPLAYER += this.PlayerDamageScoreCost;
            player.PlMain.CollisionMark = true;
        }
    }
}
