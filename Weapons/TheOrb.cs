using Microsoft.Xna.Framework;

namespace SpaceInvaderPlusPlus.Weapons
{
    internal class TheOrb : Weapon
    {
        public TheOrb(ref General general, ref Vector2 position, float angle = 0.0f)
        {
            this.WepMain = new Entity(ref general, position, angle, general.ASSETLIBRARY.tWep_Orb, null, 0.9101f);
            this.Cooldawn = 0.2f;
            this.Ammunition = 35;
            this.MaxAmmunition = 70;
            this.Damage = 4;
            this.ProjectileSprite = general.ASSETLIBRARY.tWep_Orb_Bul;
            this.FireEffect = new Entity(ref general, this.WepMain.Position, 0.0f, general.ASSETLIBRARY.tWep_Orb_Fire, 1);
            this.WepSoundEffect = general.ASSETLIBRARY.eff_Rail;
            this.Penetration = 2;
            this.AmmoScoreCost = 200;
        }

        public override void ProjectileUpdate(Vector2 shipPosition)
        {
            if (this.Projetiles == null)
                return;

            foreach (Entity entity in this.Projetiles)
            {
                entity.Angle += 0.4f;
                entity.Velocity.Y -= 1.75f;
                entity.UpdateByVelocity();
            }

        }
    }
}
