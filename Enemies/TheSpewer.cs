using Microsoft.Xna.Framework;
using SpaceInvaderPlusPlus.Players;
using System;

namespace SpaceInvaderPlusPlus.Enemies
{
    internal class TheSpewer : Enemy
    {
        private float Cooldawn { get; set; }
        private TimeSpan LastTime { get; set; }
        private string ProjectileSpriteName { get; set; }

        public TheSpewer(Vector2 position, float angle = 0.0f, string spriteName = "ene/ene_thespewer", int entityLayer = 1) : base(position, angle, spriteName, entityLayer)
        {
            this.Health = 8;
            this.MaxHealth = 8;
            this.Damage = 20;
            this.SelfCollisionDamage = 1;
            this.PlayerCollisionDamage = 80;
            this.FrontAcceleration = 0.1f;
            this.BackAcceleration = 0.1f;
            this.SideAcceleration = 0.2f;
            this.DamgeStageSpriteName = "ene/ene_thespewer_dmg";
            this.DamgeStageAnimatedPartSpriteName = "ene/ene_thespewer_pipes_dmg";
            this.AnimatedPart = new Entity(this.Position, 0.0f, "ene/ene_thespewer_pipes", 1);
            this.SelfDeathScoreCost = 500;
            this.SelfDamageScoreCost = 10;
            this.PlayerDamageScoreCost = 2;

            Cooldawn = 1;
            LastTime = TimeSpan.FromSeconds(0.0f);
            ProjectileSpriteName = "ene/ene_thespewer_bullet";
        }

        protected override void Move(Vector2 playerPosition)
        {
            //Y
            if (this.Velocity.Y < 1 && this.Position.Y < 100)
                this.Velocity.Y += this.BackAcceleration;

            else if (this.Velocity.Y > -1 && this.Position.Y > 250)
                this.Velocity.Y = -this.FrontAcceleration;


            //X long
            if (this.Position.X > playerPosition.X + 30)
            {
                if (this.Velocity.X > -2)
                    this.Velocity.X -= SideAcceleration;
                if (this.Angle < 0.2f)
                {
                    this.Angle += 0.01f;
                    this.AnimatedPart.Angle = 2 * this.Angle;
                }
            }
            else if (this.Position.X < playerPosition.X - 30)
            {
                if (this.Velocity.X < 2)
                    this.Velocity.X += this.SideAcceleration;
                if (this.Angle > -0.2f)
                {
                    this.Angle -= 0.01f;
                    this.AnimatedPart.Angle = 2 * this.Angle;
                }
            }

            //X short
            else if (this.Position.X > playerPosition.X && this.Position.X < playerPosition.X + 10)
            {
                this.Velocity.X -= SideAcceleration;
                if (this.Angle < 0.0f)
                {
                    this.Angle -= 0.01f;
                    this.AnimatedPart.Angle = 2 * this.Angle;
                }
            }
            else if (this.Position.X < playerPosition.X && this.Position.X > playerPosition.X - 10)
            {
                this.Velocity.X += this.SideAcceleration;
                if (this.Angle < 0.0f)
                {
                    this.Angle += 0.01f;
                    this.AnimatedPart.Angle = 2 * this.Angle;
                }
            }

            //COR
            if (this.Position.X < 0)
                this.Velocity.X += this.SideAcceleration * 3;
            else if (this.Position.X > Holder.WIDTH)
                this.Velocity.X -= this.SideAcceleration * 3;
        }

        protected override void Attack(Player player, GameTime gameTime = null)
        {
            if (gameTime.TotalGameTime - LastTime >= TimeSpan.FromSeconds(Cooldawn) && this.Projetiles.Count < 3 && this.Position.Y > 0)
            {
                this.Projetiles.Add(new Entity(this.Position - new Vector2(0, 25), 0.0f, ProjectileSpriteName, 1));
                LastTime = gameTime.TotalGameTime;
            }

            ProjectileUpdate(player);
        }

        private void ProjectileUpdate(Player player)
        {
            Rectangle playerBody = new Rectangle((int)player.Position.X, (int)player.Position.Y, player.EntityTexture.Width, player.EntityTexture.Height);
            for (int i = 0; i < this.Projetiles.Count; i++)
            {
                this.Projetiles[i].Position.Y += 4;
                if (this.Projetiles[i].Position.X > player.Position.X)
                {
                    this.Projetiles[i].Position.X -= 0.2f;
                    if (this.Projetiles[i].Angle < 0.05f)
                        this.Projetiles[i].Angle += 0.001f;
                }
                else if (this.Projetiles[i].Position.X < player.Position.X)
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
                    //this.Projetiles[i].CollisionMark = true;
                    player.PlayerDamage(this.Damage);
                    Holder.SCORE_DMGPLAYER += this.PlayerDamageScoreCost;
                    this.Projetiles.RemoveAt(i);
                    i--;
                    player.CollisionMark = true;
                }
                else if (this.Projetiles[i].Position.Y > Holder.HEIGHT + 100)
                    this.Projetiles.RemoveAt(i);
            }

        }

    }
}
