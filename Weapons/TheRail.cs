using Microsoft.Xna.Framework;
using SpaceInvaderPlusPlus.Utilities;
using System.Collections.Generic;

namespace SpaceInvaderPlusPlus.Weapons
{
    internal class TheRail : Weapon
    {
        public TheRail(ref General general, ref Vector2 position, float angle = 0.0f)
        {
            this.WepMain = new Entity(ref general, position, angle, general.ASSETLIBRARY.tWep_Rail, null, 0.9101f);
            this.Cooldawn = 1.5f;
            this.Ammunition = 15;
            this.MaxAmmunition = 30;
            this.Damage = 20;
            this.ProjectileSprite = general.ASSETLIBRARY.tWep_Rail_Bul;
            this.FireEffect = new Entity(ref general, this.WepMain.Position, 0.0f, general.ASSETLIBRARY.tWep_Rail_Fire, 1);
            this.WepSoundEffect = general.ASSETLIBRARY.eff_Rail;
            this.Penetration = 2;
            this.AmmoScoreCost = 3000;
        }

        public override void ProjectileUpdate(Vector2 shipPosition)
        {
            if (this.Projetiles == null)
                return;

            foreach (Entity entity in this.Projetiles)
            {
                entity.Velocity.Y -= 10.0f;
                entity.UpdateByVelocity();
            }

        }

        public override void ParticleSpawnHandling(ref General general, ref List<Particles> particles, GameTime gameTime)
        {
            foreach (Entity entity in this.Projetiles)
                particles.Add(new Particles(ref general, gameTime, 3, entity.Position, 10, new Vector2(0, 4), 8, 0.0f, 0.3f));
        }
    }
}
