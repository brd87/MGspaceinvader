﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace SpaceInvaderPlusPlus.Weapons
{
    internal class TheRail : Weapon
    {
        public TheRail(Vector2 position, float angle = 0.0f, string spriteName = "wep/wep_therail", int entityLayer = 1) : base(position, angle, spriteName, entityLayer)
        {
            this.Cooldawn = 1.5f;
            this.Ammunition = 10;
            this.MaxAmmunition = 20;
            this.Damage = 20;
            this.ProjectileSpriteName = "wep/wep_therail_bullet";
            this.FireEffect = new Entity(this.Position, 0.0f, "wep/wep_therail_fire", 1);
            this.WepSoundEffect = Holder.CONTENT.Load<SoundEffect>("eff/eff_rail");
            this.Penetration = true;
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