using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

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
            this.AmmoScoreCost = 5;
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
    }
}
