using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace SpaceInvaderPlusPlus.Utilities
{
    public class SpaceBackground
    {
        public float StarSpeed;
        public List<Entity> WhiteStars;
        public List<Entity> RedStars;
        public List<Entity> YellowStars;
        public List<Entity> BlueStars;
        public List<Entity> TheEyes;

        public SpaceBackground()
        {
            StarSpeed = 0.3f;

            WhiteStars = new List<Entity>();
            while (WhiteStars.Count < 800)
                WhiteStars.Add(new Entity(new Vector2(Holder.RANDOM.Next(0, Holder.WIDTH), Holder.RANDOM.Next(-10, Holder.HEIGHT)), 0.0f, "white_star", 1,
                    Holder.randomFloat(Holder.SCALE * 0.5f, Holder.SCALE * 1.5f)));

            RedStars = new List<Entity>();
            while (RedStars.Count < 10)
                RedStars.Add(new Entity(new Vector2(Holder.RANDOM.Next(0, Holder.WIDTH), Holder.RANDOM.Next(-10, Holder.HEIGHT)), 0.0f, "red_star", 1,
                    Holder.randomFloat(Holder.SCALE * 0.5f, Holder.SCALE * 1.5f)));

            YellowStars = new List<Entity>();
            while (YellowStars.Count < 50)
                YellowStars.Add(new Entity(new Vector2(Holder.RANDOM.Next(0, Holder.WIDTH), Holder.RANDOM.Next(-10, Holder.HEIGHT)), 0.0f, "white_star", 1,
                    Holder.randomFloat(Holder.SCALE * 0.5f, Holder.SCALE * 1.5f)));

            BlueStars = new List<Entity>();
            while (BlueStars.Count < 200)
                BlueStars.Add(new Entity(new Vector2(Holder.RANDOM.Next(0, Holder.WIDTH), Holder.RANDOM.Next(-10, Holder.HEIGHT)), 0.0f, "white_star", 1,
                    Holder.randomFloat(Holder.SCALE * 0.5f, Holder.SCALE * 1.5f)));

            TheEyes = new List<Entity>();
        }


        public void Update()
        {
            for (int i = 0; i < WhiteStars.Count; i++)
            {
                WhiteStars[i].Position.Y += StarSpeed;
                if (WhiteStars[i].Position.Y > Holder.HEIGHT + 10)
                {
                    WhiteStars.RemoveAt(i);
                    WhiteStars.Add(new Entity(new Vector2(Holder.RANDOM.Next(0, Holder.WIDTH), Holder.RANDOM.Next(-10, 0)), 0.0f, "white_star", 1,
                        Holder.randomFloat(Holder.SCALE * 0.5f, Holder.SCALE * 1.5f)));
                }
            }
            for (int i = 0; i < RedStars.Count; i++)
            {
                RedStars[i].Position.Y += StarSpeed;
                if (RedStars[i].Position.Y > Holder.HEIGHT + 10)
                {
                    RedStars.RemoveAt(i);
                    RedStars.Add(new Entity(new Vector2(Holder.RANDOM.Next(0, Holder.WIDTH), Holder.RANDOM.Next(-10, 0)), 0.0f, "red_star", 1,
                        Holder.randomFloat(Holder.SCALE * 0.5f, Holder.SCALE * 1.5f)));
                }
            }
            for (int i = 0; i < YellowStars.Count; i++)
            {
                YellowStars[i].Position.Y += StarSpeed;
                if (YellowStars[i].Position.Y > Holder.HEIGHT + 10)
                {
                    YellowStars.RemoveAt(i);
                    YellowStars.Add(new Entity(new Vector2(Holder.RANDOM.Next(0, Holder.WIDTH), Holder.RANDOM.Next(-10, 0)), 0.0f, "yellow_star", 1,
                        Holder.randomFloat(Holder.SCALE * 0.5f, Holder.SCALE * 1.5f)));
                }
            }
            for (int i = 0; i < BlueStars.Count; i++)
            {
                BlueStars[i].Position.Y += StarSpeed;
                if (BlueStars[i].Position.Y > Holder.HEIGHT + 10)
                {
                    BlueStars.RemoveAt(i);
                    BlueStars.Add(new Entity(new Vector2(Holder.RANDOM.Next(0, Holder.WIDTH), Holder.RANDOM.Next(-10, 0)), 0.0f, "blue_star", 1,
                        Holder.randomFloat(Holder.SCALE * 0.5f, Holder.SCALE * 1.5f)));
                }
            }

            if(Holder.STARTNEW)
                TheEyes.Clear();
            for (int i = 0; i < TheEyes.Count; i++)
            {
                TheEyes[i].Position.Y += StarSpeed;
                if (TheEyes[i].Position.Y > Holder.HEIGHT + 100)
                    TheEyes.RemoveAt(i);
            }
            while (TheEyes.Count < (int)Holder.SCORE_TRAVEL / 500)
                TheEyes.Add(new Entity(new Vector2(Holder.RANDOM.Next(0, Holder.WIDTH), Holder.RANDOM.Next(-10, Holder.HEIGHT)), Holder.randomFloat(-0.2f, 0.2f), "theeye", 1,
                    Holder.randomFloat(Holder.SCALE * 0.5f, Holder.SCALE * 1.5f)));
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

            foreach(var entity in TheEyes)
                entity.DrawEntity();
        }
    }
}
