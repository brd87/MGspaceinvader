using Microsoft.Xna.Framework;
using SpaceInvaderPlusPlus.Players;

namespace SpaceInvaderPlusPlus.Environments
{
    internal class AcidMine : Environmental
    {
        public AcidMine(ref General general, Vector2 position, float angle = 0.0f) : base(ref general)
        {
            this.EnvMain = new Entity(ref general, position, angle, general.ASSETLIBRARY.tEnv_Mine, null, this.Layer);
            this.EnvMain.Velocity = new Vector2(general.randomFloat(-0.2f, 0.2f), general.randomFloat(0.5f));
            this.Damage = 50;
            this.DespawnOnHit = true;
            this.ParticleSetId = 3;
            this.Armor = 0;
            this.PlayerDamageScoreCost = 100;
        }

        public override void HandleCollisionPlayer(ref General general, ref Player player)
        {
            player.PlayerDamage(ref this.Damage);
            this.EnvMain.CollisionMark = true;
            general.SCORE_DMGPLAYER += this.PlayerDamageScoreCost;
            player.PlMain.CollisionMark = true;
        }
    }
}
