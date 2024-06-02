using Microsoft.Xna.Framework;
using SpaceInvaderPlusPlus.Players;
using System;

namespace SpaceInvaderPlusPlus.Enemies
{
    internal class TheSpewer : Enemy
    {
        private float Cooldawn;
        private TimeSpan LastTime;
        private string ProjectileSpriteName;

        public TheSpewer(ref General general, Vector2 position, float angle = 0.0f, string spriteName = "ene/ene_thespewer") : base(ref general)
        {
            this.EnMain = new Entity(ref general, position, angle, spriteName, this.Layer);
            this.MaxHealth = 13;
            this.Health = this.MaxHealth;
            this.Armor = 0;
            this.Damage = 20;
            this.SelfCollisionDamage = 1;
            this.PlayerCollisionDamage = 80;
            this.FrontAcceleration = 0.1f;
            this.BackAcceleration = 0.1f;
            this.SideAcceleration = 0.2f;
            this.DamgeStageSpriteName = "ene/ene_thespewer_dmg";
            this.DamgeStageAnimatedPartSpriteName = "ene/ene_thespewer_pipes_dmg";
            this.AnimatedPart = new Entity(ref general, this.EnMain.Position, 0.0f, "ene/ene_thespewer_pipes", null, this.Layer);
            this.SelfDeathScoreCost = 500;
            this.SelfDamageScoreCost = 10;
            this.PlayerDamageScoreCost = 2;

            Cooldawn = 1;
            LastTime = TimeSpan.FromSeconds(0.0f);
            ProjectileSpriteName = "ene/ene_thespewer_bullet";
        }

        protected override void Move(ref General general, ref Vector2 playerPosition)
        {
            //Y
            if (this.EnMain.Velocity.Y < 1 && this.EnMain.Position.Y < 100)
                this.EnMain.Velocity.Y += this.BackAcceleration;

            else if (this.EnMain.Velocity.Y > -1 && this.EnMain.Position.Y > 250)
                this.EnMain.Velocity.Y = -this.FrontAcceleration;


            //X long
            if (this.EnMain.Position.X > playerPosition.X + 30)
            {
                if (this.EnMain.Velocity.X > -2)
                    this.EnMain.Velocity.X -= SideAcceleration;
                if (this.EnMain.Angle < 0.2f)
                {
                    this.EnMain.Angle += 0.01f;
                    this.AnimatedPart.Angle = 2 * this.EnMain.Angle;
                }
            }
            else if (this.EnMain.Position.X < playerPosition.X - 30)
            {
                if (this.EnMain.Velocity.X < 2)
                    this.EnMain.Velocity.X += this.SideAcceleration;
                if (this.EnMain.Angle > -0.2f)
                {
                    this.EnMain.Angle -= 0.01f;
                    this.AnimatedPart.Angle = 2 * this.EnMain.Angle;
                }
            }

            //X short
            else if (this.EnMain.Position.X > playerPosition.X && this.EnMain.Position.X < playerPosition.X + 10)
            {
                this.EnMain.Velocity.X -= SideAcceleration;
                if (this.EnMain.Angle < 0.0f)
                {
                    this.EnMain.Angle -= 0.01f;
                    this.AnimatedPart.Angle = 2 * this.EnMain.Angle;
                }
            }
            else if (this.EnMain.Position.X < playerPosition.X && this.EnMain.Position.X > playerPosition.X - 10)
            {
                this.EnMain.Velocity.X += this.SideAcceleration;
                if (this.EnMain.Angle < 0.0f)
                {
                    this.EnMain.Angle += 0.01f;
                    this.AnimatedPart.Angle = 2 * this.EnMain.Angle;
                }
            }

            //COR
            if (this.EnMain.Position.X < 0)
                this.EnMain.Velocity.X += this.SideAcceleration * 3;
            else if (this.EnMain.Position.X > general.WIDTH)
                this.EnMain.Velocity.X -= this.SideAcceleration * 3;
        }

        protected override void Attack(ref General general, ref Player player, GameTime gameTime = null)
        {
            if (gameTime.TotalGameTime - LastTime >= TimeSpan.FromSeconds(Cooldawn) && this.Projetiles.Count < 3 && this.EnMain.Position.Y > 0)
            {
                this.Projetiles.Add(new Entity(ref general, this.EnMain.Position - new Vector2(0, 25), 0.0f, ProjectileSpriteName, null, this.Layer));
                LastTime = gameTime.TotalGameTime;
            }

            ProjectileUpdate(ref general, player);
        }

        private void ProjectileUpdate(ref General general, Player player)
        {
            Rectangle playerBody = new Rectangle((int)player.PlMain.Position.X, (int)player.PlMain.Position.Y, player.PlMain.EntityTexture.Width, player.PlMain.EntityTexture.Height);
            for (int i = 0; i < this.Projetiles.Count; i++)
            {
                this.Projetiles[i].Position.Y += 4;
                if (this.Projetiles[i].Position.X > player.PlMain.Position.X)
                {
                    this.Projetiles[i].Position.X -= 0.2f;
                    if (this.Projetiles[i].Angle < 0.05f)
                        this.Projetiles[i].Angle += 0.001f;
                }
                else if (this.Projetiles[i].Position.X < player.PlMain.Position.X)
                {
                    this.Projetiles[i].Position.X += 0.2f;
                    if (this.Projetiles[i].Angle > -0.05f)
                        this.Projetiles[i].Angle -= 0.001f;
                }
                else
                {
                    if (this.Projetiles[i].Angle > 0)
                        this.Projetiles[i].Angle += 0.01f;
                    if (this.Projetiles[i].Angle < 0)
                        this.Projetiles[i].Angle -= 0.01f;
                }

                if (playerBody.Intersects(new Rectangle((int)this.Projetiles[i].Position.X, (int)this.Projetiles[i].Position.Y, this.Projetiles[i].EntityTexture.Width, this.Projetiles[i].EntityTexture.Height)))
                {
                    int damage = this.Damage;
                    player.PlayerDamage(ref damage);
                    general.SCORE_DMGPLAYER += this.PlayerDamageScoreCost;
                    this.Projetiles.RemoveAt(i);
                    i--;
                    player.PlMain.CollisionMark = true;
                }
                else if (this.Projetiles[i].Position.Y > general.HEIGHT + 100)
                    this.Projetiles.RemoveAt(i);
            }

        }

    }
}
