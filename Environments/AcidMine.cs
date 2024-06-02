using Microsoft.Xna.Framework;
using SpaceInvaderPlusPlus.Players;

namespace SpaceInvaderPlusPlus.Environments
{
    internal class AcidMine : Environmental
    {
        public AcidMine(ref General general, Vector2 position, float angle = 0.0f, string spriteName = "env/env_mine") : base(ref general)
        {
            this.EnvMain = new Entity(ref general, position, angle, spriteName, this.Layer);
            this.EnvMain.Velocity = new Vector2(general.randomFloat(-0.2f, 0.2f), general.randomFloat(0.5f));
            this.Damage = 50;
            this.DespawnOnHit = true;
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
