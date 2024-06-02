using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace SpaceInvaderPlusPlus.Weapons
{
    internal class TheGun : Weapon
    {
        public TheGun(ref General general, ref Vector2 position, float angle = 0.0f, string spriteName = "wep/wep_thegun")
        {
            this.WepMain = new Entity(ref general, position, angle, spriteName);
            this.Cooldawn = 0.1f;
            this.Ammunition = 150;
            this.MaxAmmunition = 300;
            this.Damage = 2;
            this.ProjectileSpriteName = "wep/wep_thegun_bullet";
            this.FireEffect = new Entity(ref general, this.WepMain.Position, 0.0f, "wep/wep_thegun_fire", 1);
            this.WepSoundEffect = general.CONTENT.Load<SoundEffect>("eff/eff_gun");
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
