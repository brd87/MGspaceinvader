using Microsoft.Xna.Framework;

namespace SpaceInvaderPlusPlus.Weapons
{
    internal class TheSaw : Weapon
    {
        public TheSaw(Vector2 position, float angle = 0.0f, string spriteName = "wep/wep_thesaw", int entityLayer = 1) : base(position, angle, spriteName, entityLayer)
        {
            this.Cooldawn = 0.5f;
            this.Ammunition = 20;
            this.MaxAmmunition = 40;
            this.Damage = 2;
            this.ProjectileSpriteName = "wep/wep_thesaw_bullet";
            this.FireEffect = new Entity(this.Position, 0.0f, "wep/wep_thesaw_fire", 1);
            this.Penetration = true;
            this.AmmoScoreCost = 50;
        }

        public override void ProjectileUpdate(Vector2 shipPosition)
        {
            if (this.Projetiles == null)
                return;

            foreach (Entity entity in this.Projetiles)
            {
                entity.Velocity.Y -= 0.4f;
                entity.Angle += 0.01f;
                if (shipPosition.X < entity.Position.X)
                    entity.Velocity.X -= 0.7f;
                if (shipPosition.X > entity.Position.X)
                    entity.Velocity.X += 0.7f;
                entity.UpdateByVelocity();
            }
        }
    }
}