using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace SpaceInvaderPlusPlus.Weapons
{
    internal class TheSaw : Weapon
    {
        public TheSaw(ref General general, ref Vector2 position, float angle = 0.0f, string spriteName = "wep/wep_thesaw")
        {
            this.WepMain = new Entity(ref general, position, angle, spriteName);
            this.Cooldawn = 0.5f;
            this.Ammunition = 30;
            this.MaxAmmunition = 60;
            this.Damage = 4;
            this.ProjectileSpriteName = "wep/wep_thesaw_bullet";
            this.FireEffect = new Entity(ref general, this.WepMain.Position, 0.0f, "wep/wep_thesaw_fire", 1);
            this.WepSoundEffect = general.CONTENT.Load<SoundEffect>("eff/eff_saw");
            this.Penetration = 1;
            this.AmmoScoreCost = 50;
        }

        public override void ProjectileUpdate(Vector2 shipPosition)
        {
            if (this.Projetiles == null)
                return;

            foreach (Entity entity in this.Projetiles)
            {
                entity.Velocity.Y -= 0.4f;
                entity.Angle += 0.3f;
                if (shipPosition.X < entity.Position.X)
                    entity.Velocity.X -= 0.7f;
                if (shipPosition.X > entity.Position.X)
                    entity.Velocity.X += 0.7f;
                entity.UpdateByVelocity();
            }
        }
    }
}