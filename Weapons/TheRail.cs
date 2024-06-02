using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace SpaceInvaderPlusPlus.Weapons
{
    internal class TheRail : Weapon
    {
        public TheRail(ref General general, ref Vector2 position, float angle = 0.0f, string spriteName = "wep/wep_therail")
        {
            this.WepMain = new Entity(ref general, position, angle, spriteName);
            this.Cooldawn = 1.5f;
            this.Ammunition = 15;
            this.MaxAmmunition = 30;
            this.Damage = 20;
            this.ProjectileSpriteName = "wep/wep_therail_bullet";
            this.FireEffect = new Entity(ref general, this.WepMain.Position, 0.0f, "wep/wep_therail_fire", 1);
            this.WepSoundEffect = general.CONTENT.Load<SoundEffect>("eff/eff_rail");
            this.Penetration = 2;
            this.AmmoScoreCost = 300;
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
    }
}
