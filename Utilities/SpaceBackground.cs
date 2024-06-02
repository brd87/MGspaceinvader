using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace SpaceInvaderPlusPlus.Utilities
{
    public class SpaceBackground
    {
        private float StarSpeed;
        private float RangeSpeed;
        private List<Entity> WhiteStars;
        private List<Entity> RedStars;
        private List<Entity> YellowStars;
        private List<Entity> BlueStars;

        public SpaceBackground()
        {
            StarSpeed = 0.3f;
            RangeSpeed = 0.05f;

            WhiteStars = new List<Entity>();
            for (int i = 0; i <= 800; i++)
            {
                WhiteStars.Add(new Entity(new Vector2(Holder.RANDOM.Next(0, Holder.WIDTH), Holder.RANDOM.Next(-10, Holder.HEIGHT)), 0.0f, "star/star_white",
                    Holder.randomFloat(Holder.SCALE * 0.5f, Holder.SCALE * 1.5f)));
                WhiteStars[i].Velocity.Y = Holder.randomFloat(-RangeSpeed, RangeSpeed) + StarSpeed;
            }


            RedStars = new List<Entity>();
            for (int i = 0; i <= 10; i++)
            {
                RedStars.Add(new Entity(new Vector2(Holder.RANDOM.Next(0, Holder.WIDTH), Holder.RANDOM.Next(-10, Holder.HEIGHT)), 0.0f, "star/star_red",
                    Holder.randomFloat(Holder.SCALE * 0.5f, Holder.SCALE * 1.5f)));
                RedStars[i].Velocity.Y = Holder.randomFloat(-RangeSpeed, RangeSpeed) + StarSpeed;
            }


            YellowStars = new List<Entity>();
            for (int i = 0; i <= 50; i++)
            {
                YellowStars.Add(new Entity(new Vector2(Holder.RANDOM.Next(0, Holder.WIDTH), Holder.RANDOM.Next(-10, Holder.HEIGHT)), 0.0f, "star/star_yellow",
                    Holder.randomFloat(Holder.SCALE * 0.5f, Holder.SCALE * 1.5f)));
                YellowStars[i].Velocity.Y = Holder.randomFloat(-RangeSpeed, RangeSpeed) + StarSpeed;
            }


            BlueStars = new List<Entity>();
            for (int i = 0; i <= 200; i++)
            {
                BlueStars.Add(new Entity(new Vector2(Holder.RANDOM.Next(0, Holder.WIDTH), Holder.RANDOM.Next(-10, Holder.HEIGHT)), 0.0f, "star/star_blue",
                    Holder.randomFloat(Holder.SCALE * 0.5f, Holder.SCALE * 1.5f)));
                BlueStars[i].Velocity.Y = Holder.randomFloat(-RangeSpeed, RangeSpeed) + StarSpeed;
            }

        }


        public void Update()
        {
            for (int i = 0; i < WhiteStars.Count; i++)
            {
                WhiteStars[i].UpdateByVelocity();
                if (WhiteStars[i].Position.Y > Holder.HEIGHT + 10)
                {
                    WhiteStars.RemoveAt(i);
                    WhiteStars.Add(new Entity(new Vector2(Holder.RANDOM.Next(0, Holder.WIDTH), Holder.RANDOM.Next(-10, 0)), 0.0f, "star/star_white",
                        Holder.randomFloat(Holder.SCALE * 0.5f, Holder.SCALE * 1.5f)));
                    WhiteStars[WhiteStars.Count - 1].Velocity.Y = Holder.randomFloat(-RangeSpeed, RangeSpeed) + StarSpeed;
                    i--;
                }
            }
            for (int i = 0; i < RedStars.Count; i++)
            {
                RedStars[i].UpdateByVelocity();
                if (RedStars[i].Position.Y > Holder.HEIGHT + 10)
                {
                    RedStars.RemoveAt(i);
                    RedStars.Add(new Entity(new Vector2(Holder.RANDOM.Next(0, Holder.WIDTH), Holder.RANDOM.Next(-10, 0)), 0.0f, "star/star_red",
                        Holder.randomFloat(Holder.SCALE * 0.5f, Holder.SCALE * 1.5f)));
                    RedStars[RedStars.Count - 1].Velocity.Y = Holder.randomFloat(-RangeSpeed, RangeSpeed) + StarSpeed;
                    i--;
                }
            }
            for (int i = 0; i < YellowStars.Count; i++)
            {
                YellowStars[i].UpdateByVelocity();
                if (YellowStars[i].Position.Y > Holder.HEIGHT + 10)
                {
                    YellowStars.RemoveAt(i);
                    YellowStars.Add(new Entity(new Vector2(Holder.RANDOM.Next(0, Holder.WIDTH), Holder.RANDOM.Next(-10, 0)), 0.0f, "star/star_yellow",
                        Holder.randomFloat(Holder.SCALE * 0.5f, Holder.SCALE * 1.5f)));
                    YellowStars[YellowStars.Count - 1].Velocity.Y = Holder.randomFloat(-RangeSpeed, RangeSpeed) + StarSpeed;
                    i--;
                }
            }
            for (int i = 0; i < BlueStars.Count; i++)
            {
                BlueStars[i].UpdateByVelocity();
                if (BlueStars[i].Position.Y > Holder.HEIGHT + 10)
                {
                    BlueStars.RemoveAt(i);
                    BlueStars.Add(new Entity(new Vector2(Holder.RANDOM.Next(0, Holder.WIDTH), Holder.RANDOM.Next(-10, 0)), 0.0f, "star/star_blue",
                        Holder.randomFloat(Holder.SCALE * 0.5f, Holder.SCALE * 1.5f)));
                    BlueStars[BlueStars.Count - 1].Velocity.Y = Holder.randomFloat(-RangeSpeed, RangeSpeed) + StarSpeed;
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
