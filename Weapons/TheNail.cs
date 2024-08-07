using Microsoft.Xna.Framework;
using SpaceInvaderPlusPlus.Utilities;
using System.Collections.Generic;

namespace SpaceInvaderPlusPlus.Weapons
{
    internal class TheNail : Weapon
    {
        public TheNail(ref General general, ref Vector2 position, float angle = 0.0f)
        {
            this.WepMain = new Entity(ref general, position, angle, general.ASSETLIBRARY.tWep_Nail, null, 0.9101f);
            this.Cooldawn = 1.0f;
            this.BurstCooldawn = 0.15f;
            this.BurstAmount = 5;
            this.Ammunition = 70;
            this.MaxAmmunition = 140;
            this.Damage = 2;
            this.ProjectileSprite = general.ASSETLIBRARY.tWep_Nail_Bul;
            this.FireEffect = new Entity(ref general, this.WepMain.Position, 0.0f, general.ASSETLIBRARY.tWep_Nail_Fire, 1);
            this.WepSoundEffect = general.ASSETLIBRARY.eff_Saw;
            this.Penetration = 1;
            this.AmmoScoreCost = 50;
        }

        public override void ProjectileUpdate(Vector2 shipPosition)
        {
            if (this.Projetiles == null)
                return;

            foreach (Entity entity in this.Projetiles)
            {
                entity.Velocity.Y -= 0.5f;
                if (shipPosition.X < entity.Position.X)
                {
                    entity.Velocity.X -= 0.4f;
                    if (entity.Angle > -0.3f)
                        entity.Angle -= 0.02f;
                }

                if (shipPosition.X > entity.Position.X)
                {
                    entity.Velocity.X += 0.4f;
                    if (entity.Angle < 0.3f)
                        entity.Angle += 0.02f;
                }

                entity.UpdateByVelocity();
            }
        }

        public override void ParticleSpawnHandling(ref General general, ref List<Particles> particles, GameTime gameTime)
        {
            if (this.FireGranted)
                particles.Add(new Particles(ref general, gameTime, 3, this.WepMain.Position, 5, new Vector2(0, -5), 7, 1.2f, 0.4f));
        }
    }
}
