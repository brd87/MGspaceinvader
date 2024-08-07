using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        public bool EndAll { get; set; }

        public Particles(ref General general, GameTime gameTime, int amount, Vector2 center, int range, Vector2 generalVelocity, int set = 0, float sideSpread = 1.0f, float cooldawn = 5.0f)
        {
            Spawned = gameTime.TotalGameTime;
            Cooldawn = cooldawn;
            Layer = 0.4f;
            EndAll = false;
            if (amount > 20) amount = 20;
            List<Texture2D> spriteNames;
            if (set == 1)
                spriteNames = new List<Texture2D>() { general.ASSETLIBRARY.tRem_p1, general.ASSETLIBRARY.tRem_p2, general.ASSETLIBRARY.tRem_p3, general.ASSETLIBRARY.tRem_p5 };
            else if (set == 2)
                spriteNames = new List<Texture2D>() { general.ASSETLIBRARY.tRem_p1, general.ASSETLIBRARY.tRem_p2, general.ASSETLIBRARY.tRem_p3, general.ASSETLIBRARY.tRem_p6 };
            else if (set == 3)
                spriteNames = new List<Texture2D>() { general.ASSETLIBRARY.tRem_p6 };
            else if (set == 4)
                spriteNames = new List<Texture2D>() { general.ASSETLIBRARY.tRem_p7 };
            else if (set == 5)
                spriteNames = new List<Texture2D>() { general.ASSETLIBRARY.tOther_shellGun };
            else if (set == 6)
                spriteNames = new List<Texture2D>() { general.ASSETLIBRARY.tOther_shellDuo };
            else if (set == 7)
                spriteNames = new List<Texture2D>() { general.ASSETLIBRARY.tOther_spark };
            else if (set == 8)
                spriteNames = new List<Texture2D>() { general.ASSETLIBRARY.tOther_plasma };
            else
                spriteNames = new List<Texture2D>() { general.ASSETLIBRARY.tRem_p1, general.ASSETLIBRARY.tRem_p2, general.ASSETLIBRARY.tRem_p3, general.ASSETLIBRARY.tRem_p4 };

            Parts = new List<Entity>();
            Torques = new List<float>();
            int minX = (int)center.X - range;
            int maxX = (int)center.X + range;
            int minY = (int)center.Y - range;
            int maxY = (int)center.Y + range;
            for (int i = 0; i <= amount; i++)
            {
                Parts.Add(new Entity(ref general, new Vector2(general.RANDOM.Next(minX, maxX), general.RANDOM.Next(minY, maxY)), general.randomFloat(-0.5f, 0.5f), spriteNames[general.RANDOM.Next(0, spriteNames.Count)],
                    general.randomFloat(general.SCALE * 0.7f, general.SCALE * 1.3f), Layer));
                Parts[i].Velocity = new Vector2(general.randomFloat(-2, 2) * sideSpread, general.randomFloat(-3, 2)) + generalVelocity;
                Torques.Add(general.randomFloat(-0.01f, 0.01f));
            }
        }

        public void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime - Spawned >= TimeSpan.FromSeconds(Cooldawn))
            {
                EndAll = true;
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
            foreach (Entity part in Parts)
                part.DrawEntity(ref general);
        }
    }
}
