using Microsoft.Xna.Framework;
using SpaceInvaderPlusPlus.Players;

namespace SpaceInvaderPlusPlus.Utilities
{
    internal class EnemyProjectile
    {
        public Entity ProMain { get; set; }
        protected float PlayerDamageScoreCost;
        private int Damage;
        private float Layer;

        public EnemyProjectile(ref General general, ref Vector2 SpawnPos)
        {
            PlayerDamageScoreCost = 500;
            Damage = 20;
            Layer = 0.498f;
            ProMain = new Entity(ref general, SpawnPos, 0.0f, general.ASSETLIBRARY.tEneProjectile, null, this.Layer);
        }
        public void Update(ref General general, ref Player player)
        {
            ProMain.Position.Y += 4;
            if (ProMain.Position.X > player.PlMain.Position.X)
            {
                ProMain.Position.X -= 0.25f;
                if (ProMain.Angle < 0.05f)
                    ProMain.Angle += 0.001f;
            }
            else if (ProMain.Position.X < player.PlMain.Position.X)
            {
                ProMain.Position.X += 0.25f;
                if (ProMain.Angle > -0.05f)
                    ProMain.Angle -= 0.001f;
            }
            else
            {
                if (ProMain.Angle > 0)
                    ProMain.Angle += 0.01f;
                if (ProMain.Angle < 0)
                    ProMain.Angle -= 0.01f;
            }

            if (Vector2.Distance(this.ProMain.Position, player.PlMain.Position) < this.ProMain.EntityTexture.Height / 2 + player.PlMain.EntityTexture.Height / 2)
            {
                int damage = this.Damage;
                player.PlayerDamage(ref damage);
                general.SCORE_DMGPLAYER += this.PlayerDamageScoreCost;
                ProMain.CollisionMark = true;
            }
        }
    }
}
