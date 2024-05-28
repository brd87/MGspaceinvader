using Microsoft.Xna.Framework;

namespace SpaceInvaderPlusPlus.Weapons
{
    public class TheGun : Weapon
    {
        public TheGun(Vector2 position, float angle = 0.0f, string spriteName = "thegun", int entityLayer = 1) : base(position, angle, spriteName, entityLayer)
        {
            this.Cooldawn = 0.05f;
            this.Ammunition = 100;
            this.MaxAmmunition = 200;
            this.Damage = 1;
            this.ProjectileSpriteName = "thegun_bullet";
            this.FireEffect = new Entity(this.Position, 0.0f, "thegun_fire", 1);
            this.Penetration = false;
            this.AmmoScoreCost = 5;
        }

        public override void ProjectileUpdate()
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
