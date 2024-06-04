using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaderPlusPlus.Players;

namespace SpaceInvaderPlusPlus
{
    internal abstract class Enemy
    {
        public Entity EnMain { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Armor { get; set; }
        public int ParticleSetId { get; set; }
        protected int Damage;
        protected int SelfCollisionDamage;
        protected int PlayerCollisionDamage;
        protected float FrontAcceleration;
        protected float SideAcceleration;
        protected float BackAcceleration;
        protected Texture2D DamgeStageSpriteName;
        protected Texture2D DamgeStageAnimatedPartSpriteName;
        private bool DamgeStageCheck;
        protected Entity AnimatedPart;
        private SoundEffectInstance DeathSoundEffectIns;
        protected float SelfDeathScoreCost;
        protected float SelfDamageScoreCost;
        protected float PlayerDamageScoreCost;
        protected float PlayerCollisionScoreCost;

        public bool AskTofire { get; set; }
        public bool UltRecived { get; set; }
        protected float Layer;

        protected Enemy(ref General general)
        {
            DamgeStageCheck = false;
            UltRecived = false;
            DeathSoundEffectIns = general.CONTENT.Load<SoundEffect>("eff/eff_death").CreateInstance();
            DeathSoundEffectIns.Volume = general.SETTINGS.LastEffectsVolume;
            AskTofire = false;
            Layer = 0.5f;
        }

        public void Update(ref General general, ref Player player, ref Weapon weapon, GameTime gameTime = null)
        {
            if (this.EnMain.CollisionMark)
                this.EnMain.CollisionMark = false;

            Move(ref general, ref player.PlMain.Position);
            EnMain.UpdateByVelocity();
            this.AnimatedPart.Position = this.EnMain.Position;
            CollisionCheck(ref general, ref player, ref weapon);
            Attack(ref general, ref player, gameTime);
            if (Health <= 0) general.SCORE_DMG += SelfDeathScoreCost;
        }

        private void CollisionCheck(ref General general, ref Player player, ref Weapon weapon)
        {
            if (Vector2.Distance(this.EnMain.Position, player.PlMain.Position) < this.EnMain.EntityTexture.Height / 2 + player.PlMain.EntityTexture.Height / 2)
            {
                this.EnMain.CollisionMark = true;
                Health -= SelfCollisionDamage;
                player.PlayerDamage(ref PlayerCollisionDamage);
                general.SCORE_DMGPLAYER += PlayerCollisionScoreCost;
            }
            foreach (Entity projectile in weapon.Projetiles)
            {
                if (Vector2.Distance(this.EnMain.Position, projectile.Position) < this.EnMain.EntityTexture.Height / 3 * 2 + projectile.EntityTexture.Height / 3)
                {
                    if (!projectile.CollisionMark || weapon.Penetration >= Armor)
                    {
                        Health -= weapon.Damage;
                        general.SCORE_DMG += SelfDamageScoreCost;
                    }

                    if (weapon.Penetration <= Armor)
                        projectile.CollisionMark = true;
                }
            }

            if (Health <= MaxHealth / 2)
            {
                if (!DamgeStageCheck)
                {
                    this.EnMain.UpdateSprite(DamgeStageSpriteName);
                    this.AnimatedPart.UpdateSprite(DamgeStageAnimatedPartSpriteName);
                    DamgeStageCheck = true;
                }

                if (Health <= 0)
                    DeathSoundEffectIns.Play();
            }
        }

        protected abstract void Move(ref General general, ref Vector2 playerPosition);

        protected abstract void Attack(ref General general, ref Player player, GameTime gameTime = null);

        public void DrawAll(ref General general)
        {
            AnimatedPart.DrawEntity(ref general);
            this.EnMain.DrawEntity(ref general);
        }
    }
}