using Microsoft.Xna.Framework;

namespace SpaceInvaderPlusPlus.Weapons
{
    internal class TheSaw : Weapon
    {
        public TheSaw(ref General general, ref Vector2 position, float angle = 0.0f)
        {
            this.WepMain = new Entity(ref general, position, angle, general.ASSETLIBRARY.tWep_Saw, null, 0.9101f);
            this.Cooldawn = 0.5f;
            this.Ammunition = 30;
            this.MaxAmmunition = 60;
            this.Damage = 4;
            this.ProjectileSprite = general.ASSETLIBRARY.tWep_Saw_Bul;
            this.FireEffect = new Entity(ref general, this.WepMain.Position, 0.0f, general.ASSETLIBRARY.tWep_Saw_Fire, 1);
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