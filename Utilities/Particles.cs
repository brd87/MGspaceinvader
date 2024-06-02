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
        private float Layer;
        public bool EndAll {  get; set; }

        public Particles(ref General general, GameTime gameTime, int amount, Vector2 center, int range, Vector2 generalVelocity, int set = 0) 
        { 
            Spawned = gameTime.TotalGameTime;
            Cooldawn = 10;
            Layer = 0.2f;
            EndAll = false;
            if(amount > 20) amount = 20;
            List<string> spriteNames;
            if (set == 1)
                spriteNames = new List<string>() { "rem/rem_p1", "rem/rem_p2", "rem/rem_p3", "rem/rem_p5" };
            else if (set == 2)
                spriteNames = new List<string>() { "rem/rem_p1", "rem/rem_p2", "rem/rem_p3", "rem/rem_p6" };
            else
                spriteNames = new List<string>() { "rem/rem_p1", "rem/rem_p2", "rem/rem_p3", "rem/rem_p4" };

            Parts = new List<Entity>();
            Torques = new List<float>();
            int minX = (int)center.X - range;
            int maxX = (int)center.X + range;
            int minY = (int)center.Y - range;
            int maxY = (int)center.Y + range;
            for (int i = 0; i <= amount; i++)
            {
                Parts.Add(new Entity(ref general, new Vector2(general.RANDOM.Next(minX, maxX), general.RANDOM.Next(minY, maxY)), general.randomFloat(-0.5f, 0.5f), spriteNames[general.RANDOM.Next(0, 4)],
                    general.randomFloat(general.SCALE * 0.7f, general.SCALE * 1.3f), Layer));
                Parts[i].Velocity = new Vector2(general.RANDOM.Next(-3, 3), general.RANDOM.Next(-3, 3)) + generalVelocity;
                Torques.Add(general.randomFloat(-0.02f, 0.02f));
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

        public void DrawAll(ref General general)
        {
            foreach(Entity part in Parts)
                part.DrawEntity(ref general);
        }
    }
}
