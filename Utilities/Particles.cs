using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace SpaceInvaderPlusPlus.Utilities
{
    internal class Particles
    {
        private List<Entity> Parts;
        private List<float> Torques;
        private TimeSpan Spawned;
        private float Cooldawn;
        public bool EndAll {  get; set; }

        public Particles(GameTime gameTime, int amount, Vector2 center, int range, Vector2 generalVelocity) 
        { 
            Spawned = gameTime.TotalGameTime;
            Cooldawn = 10;
            EndAll = false;
            if(amount > 20) amount = 20;
            List<string> spriteNames = new List<string>() { "rem/rem_p1", "rem/rem_p2", "rem/rem_p3", "rem/rem_p4" };
            Parts = new List<Entity>();
            Torques = new List<float>();
            int minX = (int)center.X - range;
            int maxX = (int)center.X + range;
            int minY = (int)center.Y - range;
            int maxY = (int)center.Y + range;
            for (int i = 0; i <= amount; i++)
            {
                Parts.Add(new Entity(new Vector2(Holder.RANDOM.Next(minX, maxX), Holder.RANDOM.Next(minY, maxY)), Holder.randomFloat(-0.5f, 0.5f), spriteNames[Holder.RANDOM.Next(0, 4)], 
                    Holder.randomFloat(Holder.SCALE * 0.7f, Holder.SCALE * 1.3f)));
                Parts[i].Velocity = new Vector2(Holder.RANDOM.Next(-3, 3), Holder.RANDOM.Next(-3, 3)) + generalVelocity;
                Torques.Add(Holder.randomFloat(-0.02f, 0.02f));
            }
        }

        public void Update(GameTime gameTime)
        {
            if(gameTime.TotalGameTime - Spawned >= TimeSpan.FromSeconds(Cooldawn))
            {
                EndAll = true;
                Parts.Clear();
                return;
            }
            for (int i = 0; i < Parts.Count; i++)
            {
                Parts[i].UpdateByVelocity();
                Parts[i].Angle += Torques[i];
            }

        }

        public void DrawAll()
        {
            foreach(Entity part in Parts)
                part.DrawEntity();
        }
    }
}
