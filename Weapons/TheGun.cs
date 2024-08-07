using Microsoft.Xna.Framework;
using SpaceInvaderPlusPlus.Utilities;
using System.Collections.Generic;

namespace SpaceInvaderPlusPlus.Weapons
{
    internal class TheGun : Weapon
    {
        public TheGun(ref General general, ref Vector2 position, float angle = 0.0f)
        {
            this.WepMain = new Entity(ref general, position, angle, general.ASSETLIBRARY.tWep_Gun, null, 0.9101f);
            this.Cooldawn = 0.1f;
            this.Ammunition = 150;
            this.MaxAmmunition = 300;
            this.Damage = 2;
            this.ProjectileSprite = general.ASSETLIBRARY.tWep_Gun_Bul;
            this.FireEffect = new Entity(ref general, this.WepMain.Position, 0.0f, general.ASSETLIBRARY.tWep_Gun_Fire, 1);
            this.WepSoundEffect = general.ASSETLIBRARY.eff_Gun;
            this.Penetration = 0;
            this.AmmoScoreCost = 20;
        }

        public override void ProjectileUpdate(Vector2 shipPosition)
        {
            if (this.Projetiles == null)
                return;

            foreach (Entity entity in this.Projetiles)
            {
                entity.Position.Y -= 30.0f;
            }
        }

        public override void ParticleSpawnHandling(ref General general, ref List<Particles> particles, GameTime gameTime)
        {
            if(this.FireGranted)
                particles.Add(new Particles(ref general, gameTime, 0, this.WepMain.Position, 0, new Vector2(0, 5), 5, 0.3f));
            foreach (Entity entity in this.Projetiles)
                if(entity.CollisionMark)
                    particles.Add(new Particles(ref general, gameTime, 2, entity.Position, 4, new Vector2(0, 4), 7, 3.0f, 0.2f));
        }
    }
}
