using Microsoft.Xna.Framework;

namespace SpaceInvaderPlusPlus.Weapons
{
    internal class TheDuo : Weapon
    {
        public TheDuo(ref General general, ref Vector2 position, float angle = 0.0f)
        {
            this.WepMain = new Entity(ref general, position, angle, general.ASSETLIBRARY.tWep_Duo, null, 0.9101f);
            this.Cooldawn = 0.6f;
            this.BurstCooldawn = 0.07f;
            this.BurstAmount = 2;
            this.Ammunition = 60;
            this.MaxAmmunition = 120;
            this.Damage = 7;
            this.ProjectileSprite = general.ASSETLIBRARY.tWep_Duo_Bul;
            this.FireEffect = new Entity(ref general, this.WepMain.Position, 0.0f, general.ASSETLIBRARY.tWep_Duo_Fire, 1);
            this.WepSoundEffect = general.ASSETLIBRARY.eff_Gun;
            this.Penetration = 0;
            this.AmmoScoreCost = 200;
        }

        public override void ProjectileUpdate(Vector2 shipPosition)
        {
            if (this.Projetiles == null)
                return;

            foreach (Entity entity in this.Projetiles)
            {
                entity.Position.Y -= 40.0f;
            }
        }
    }
}
