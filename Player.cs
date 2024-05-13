using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceInvaderPlusPlus
{
    public class Player : Entity
    {
        PlayerPart PlFront;
        PlayerPart PlLeft;
        PlayerPart PlRight;

        public int Health { get; set; }
        public int Shields { get; set; }

        public float FrontAcceleration { get; set; }
        public float SideAcceleration { get; set; }
        public float BackAcceleration { get; set; }
        public float Stabilisers {  get; set; }
        public float Drag {  get; set; }

        public bool AskToFire { get; set; }

        public Vector2 Velocity;

        public Player(int dif, Vector2 position, float angle = 0.0f, string spriteName = "player", int entityLayer = 1) : 
            base(position, angle, spriteName, entityLayer)
        {
            PlFront = new PlayerPart(new Vector2(Holder.width / 2, Holder.height / 4 * 3), "player_front");
            PlLeft = new PlayerPart(new Vector2(Holder.width / 2, Holder.height / 4 * 3), "player_lwing");
            PlRight = new PlayerPart(new Vector2(Holder.width / 2, Holder.height / 4 * 3), "player_rwing");

            Velocity = new Vector2(0.0f, 0.0f);

            Health = 100;
            Shields = 100;
            FrontAcceleration = 0.5f;
            SideAcceleration = 0.3f;
            BackAcceleration = 0.4f;
            Stabilisers = 0.1f;
            Drag = 0.2f;
            if (dif == 0)
            {
                Holder.difMultiplyier = 0.5f;
                Health *= 2;
                Shields *= 2;
                FrontAcceleration *= 1.2f;
                SideAcceleration *= 1.5f;
                BackAcceleration *= 1.2f;
                Stabilisers *= 1.5f;
            }
            else if (dif == 1)
            {
                Holder.difMultiplyier = 1;
                //defoult movement
            }
            else if(dif == 2)
            {
                Holder.difMultiplyier = 1.5f;
                Health = 75;
                Shields = 75;
                FrontAcceleration *= 0.75f;
                SideAcceleration *= 0.75f;
                BackAcceleration *= 0.75f;
                Stabilisers *= 0.5f;
            }
            else
            {
                Holder.difMultiplyier = 2;
                Health = 50;
                Shields = 50;
                FrontAcceleration *= 0.6f;
                SideAcceleration *= 0.5f;
                BackAcceleration *= 0.6f;
                Stabilisers *= 0.3f;
            }
                
        }

        public void Update(KeyboardState kState)
        {
            AskToFire = false;
            if (kState.IsKeyDown(Keys.Space))
            {
                AskToFire = true;
            }

            if (kState.IsKeyDown(Keys.W))
            {
                Velocity.Y -= FrontAcceleration;
                this.Position += Velocity;
            }
            if (kState.IsKeyDown(Keys.S))
            {
                Velocity.Y += BackAcceleration;
                this.Position += Velocity;
            }
            if (kState.IsKeyDown(Keys.A))
            {
                Velocity.X -= SideAcceleration;
                this.Position += Velocity;
                if(Angle > -0.2f)
                    Angle -= 0.01f;
            }
            if (kState.IsKeyDown(Keys.D))
            {
                Velocity.X += SideAcceleration;
                this.Position += Velocity;
                if (Angle < 0.2f)
                    Angle += 0.01f;
            }

            if (kState.IsKeyUp(Keys.Space))
            {
                AskToFire = false;
            }

            if (kState.IsKeyUp(Keys.W) && kState.IsKeyUp(Keys.S))
            {
                if(Velocity.Y < (0.3f))
                    Velocity.Y += Drag;
                if(Velocity.Y > (-0.2f))
                    Velocity.Y -= Stabilisers;
                this.Position += Velocity;
            }
            if (kState.IsKeyUp(Keys.A) && kState.IsKeyUp(Keys.D))
            {
                if (Velocity.X > (0.3f))
                    Velocity.X -= Stabilisers;
                if (Velocity.X < (-0.3f))
                    Velocity.X += Stabilisers;
                if (Angle > 0.0f)
                    Angle -= 0.01f;
                if (Angle < 0.0f)
                    Angle += 0.01f;
                this.Position += Velocity;
            }



            if(this.Position.X < 0)
            {
                this.Position.X = 0;
                Velocity.X = 1;
            }
                
            if(this.Position.X > Holder.width)
            {
                this.Position.X = Holder.width;
                Velocity.X = -2;
            }
                
            if (this.Position.Y < 0)
            {
                this.Position.Y = 0;
                Velocity.Y = 10;
            }
                
            if (this.Position.Y > Holder.height)
            {
                this.Position.Y = Holder.height;
                Velocity.Y = -2;
            }

            PlFront.Update(this.Position, kState);
            PlLeft.Update(this.Position, kState);
            PlRight.Update(this.Position, kState);
        }

        public void DrawAll()
        {
            PlFront.DrawEntity();
            PlLeft.DrawEntity();
            PlRight.DrawEntity();
            this.DrawEntity();
        }

        public void HandleCollision(Entity entity)
        {
            // Logic for handling collisions with enemies and environmental hazards
        }
    }
}
