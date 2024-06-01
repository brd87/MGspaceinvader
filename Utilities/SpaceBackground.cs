using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace SpaceInvaderPlusPlus.Utilities
{
    public class SpaceBackground
    {
        private float StarSpeed;
        private List<Entity> WhiteStars;
        private List<Entity> RedStars;
        private List<Entity> YellowStars;
        private List<Entity> BlueStars;

        public SpaceBackground()
        {
            StarSpeed = 0.3f;

            WhiteStars = new List<Entity>();
            while (WhiteStars.Count < 800)
                WhiteStars.Add(new Entity(new Vector2(Holder.RANDOM.Next(0, Holder.WIDTH), Holder.RANDOM.Next(-10, Holder.HEIGHT)), 0.0f, "star/star_white", 1,
                    Holder.randomFloat(Holder.SCALE * 0.5f, Holder.SCALE * 1.5f)));

            RedStars = new List<Entity>();
            while (RedStars.Count < 10)
                RedStars.Add(new Entity(new Vector2(Holder.RANDOM.Next(0, Holder.WIDTH), Holder.RANDOM.Next(-10, Holder.HEIGHT)), 0.0f, "star/star_red", 1,
                    Holder.randomFloat(Holder.SCALE * 0.5f, Holder.SCALE * 1.5f)));

            YellowStars = new List<Entity>();
            while (YellowStars.Count < 50)
                YellowStars.Add(new Entity(new Vector2(Holder.RANDOM.Next(0, Holder.WIDTH), Holder.RANDOM.Next(-10, Holder.HEIGHT)), 0.0f, "star/star_yellow", 1,
                    Holder.randomFloat(Holder.SCALE * 0.5f, Holder.SCALE * 1.5f)));

            BlueStars = new List<Entity>();
            while (BlueStars.Count < 200)
                BlueStars.Add(new Entity(new Vector2(Holder.RANDOM.Next(0, Holder.WIDTH), Holder.RANDOM.Next(-10, Holder.HEIGHT)), 0.0f, "star/star_blue", 1,
                    Holder.randomFloat(Holder.SCALE * 0.5f, Holder.SCALE * 1.5f)));
        }


        public void Update()
        {
            for (int i = 0; i < WhiteStars.Count; i++)
            {
                WhiteStars[i].Position.Y += StarSpeed;
                if (WhiteStars[i].Position.Y > Holder.HEIGHT + 10)
                {
                    WhiteStars.RemoveAt(i);
                    WhiteStars.Add(new Entity(new Vector2(Holder.RANDOM.Next(0, Holder.WIDTH), Holder.RANDOM.Next(-10, 0)), 0.0f, "star/star_white", 1,
                        Holder.randomFloat(Holder.SCALE * 0.5f, Holder.SCALE * 1.5f)));
                    i--;
                }
            }
            for (int i = 0; i < RedStars.Count; i++)
            {
                RedStars[i].Position.Y += StarSpeed;
                if (RedStars[i].Position.Y > Holder.HEIGHT + 10)
                {
                    RedStars.RemoveAt(i);
                    RedStars.Add(new Entity(new Vector2(Holder.RANDOM.Next(0, Holder.WIDTH), Holder.RANDOM.Next(-10, 0)), 0.0f, "star/star_red", 1,
                        Holder.randomFloat(Holder.SCALE * 0.5f, Holder.SCALE * 1.5f)));
                    i--;
                }
            }
            for (int i = 0; i < YellowStars.Count; i++)
            {
                YellowStars[i].Position.Y += StarSpeed;
                if (YellowStars[i].Position.Y > Holder.HEIGHT + 10)
                {
                    YellowStars.RemoveAt(i);
                    YellowStars.Add(new Entity(new Vector2(Holder.RANDOM.Next(0, Holder.WIDTH), Holder.RANDOM.Next(-10, 0)), 0.0f, "star/star_yellow", 1,
                        Holder.randomFloat(Holder.SCALE * 0.5f, Holder.SCALE * 1.5f)));
                    i--;
                }
            }
            for (int i = 0; i < BlueStars.Count; i++)
            {
                BlueStars[i].Position.Y += StarSpeed;
                if (BlueStars[i].Position.Y > Holder.HEIGHT + 10)
                {
                    BlueStars.RemoveAt(i);
                    BlueStars.Add(new Entity(new Vector2(Holder.RANDOM.Next(0, Holder.WIDTH), Holder.RANDOM.Next(-10, 0)), 0.0f, "star/star_blue", 1,
                        Holder.randomFloat(Holder.SCALE * 0.5f, Holder.SCALE * 1.5f)));
                    i--;
                }
            }
        }

        public void DrawAll()
        {
            foreach (var entity in WhiteStars)
                entity.DrawEntity();

            foreach (var entity in RedStars)
                entity.DrawEntity();

            foreach (var entity in YellowStars)
                entity.DrawEntity();

            foreach (var entity in BlueStars)
                entity.DrawEntity();
        }
    }
}
