using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace SpaceInvaderPlusPlus.Utilities
{
    public class SpaceBackground
    {
        private float StarSpeed;
        private float RangeSpeed;
        private Entity[] WhiteStars;
        private Entity[] RedStars;
        private Entity[] YellowStars;
        private Entity[] BlueStars;
        private int WhiteAm;
        private int RedAm;
        private int YellowAm;
        private int BlueAm;
        private float Layer;

        public SpaceBackground(ref General general)
        {
            StarSpeed = 0.3f;
            RangeSpeed = 0.05f;

            WhiteAm = 800;
            RedAm = 10;
            YellowAm = 50;
            BlueAm = 200;
            Layer = 0.1f;

            WhiteStars = new Entity[800];
            RedStars = new Entity[10];
            YellowStars = new Entity[50];
            BlueStars = new Entity[200];

            for (int i = 0; i < WhiteAm; i++)
            {
                WhiteStars[i] = new Entity(ref general, new Vector2(general.RANDOM.Next(0, general.WIDTH), general.RANDOM.Next(-10, general.HEIGHT)), 0.0f, "star/star_white",
                    general.randomFloat(general.SCALE * 0.5f, general.SCALE * 1.5f), Layer);
                WhiteStars[i].Velocity.Y = general.randomFloat(-RangeSpeed, RangeSpeed) + StarSpeed;
            }
            for (int i = 0; i < RedAm; i++)
            {
                RedStars[i] = new Entity(ref general, new Vector2(general.RANDOM.Next(0, general.WIDTH), general.RANDOM.Next(-10, general.HEIGHT)), 0.0f, "star/star_red",
                    general.randomFloat(general.SCALE * 0.5f, general.SCALE * 1.5f), Layer);
                RedStars[i].Velocity.Y = general.randomFloat(-RangeSpeed, RangeSpeed) + StarSpeed;
            }
            for (int i = 0; i < YellowAm; i++)
            {
                YellowStars[i] = new Entity(ref general, new Vector2(general.RANDOM.Next(0, general.WIDTH), general.RANDOM.Next(-10, general.HEIGHT)), 0.0f, "star/star_yellow",
                    general.randomFloat(general.SCALE * 0.5f, general.SCALE * 1.5f), Layer);
                YellowStars[i].Velocity.Y = general.randomFloat(-RangeSpeed, RangeSpeed) + StarSpeed;
            }
            for (int i = 0; i < BlueAm; i++)
            {
                BlueStars[i] = new Entity(ref general, new Vector2(general.RANDOM.Next(0, general.WIDTH), general.RANDOM.Next(-10, general.HEIGHT)), 0.0f, "star/star_blue",
                    general.randomFloat(general.SCALE * 0.5f, general.SCALE * 1.5f), Layer);
                BlueStars[i].Velocity.Y = general.randomFloat(-RangeSpeed, RangeSpeed) + StarSpeed;
            }
        }


        public void Update(ref General general)
        {
            for (int i = 0; i < WhiteAm; i++)
            {
                WhiteStars[i].UpdateByVelocity();
                if (WhiteStars[i].Position.Y > general.HEIGHT + 10)
                {
                    WhiteStars[i] = new Entity(ref general, new Vector2(general.RANDOM.Next(0, general.WIDTH), general.RANDOM.Next(-10, 0)), 0.0f, "star/star_white",
                        general.randomFloat(general.SCALE * 0.5f, general.SCALE * 1.5f), Layer);
                    WhiteStars[i].Velocity.Y = general.randomFloat(-RangeSpeed, RangeSpeed) + StarSpeed;
                }
            }
            for (int i = 0; i < RedAm; i++)
            {
                RedStars[i].UpdateByVelocity();
                if (RedStars[i].Position.Y > general.HEIGHT + 10)
                {
                    RedStars[i] = new Entity(ref general, new Vector2(general.RANDOM.Next(0, general.WIDTH), general.RANDOM.Next(-10, 0)), 0.0f, "star/star_red",
                        general.randomFloat(general.SCALE * 0.5f, general.SCALE * 1.5f), Layer);
                    RedStars[i].Velocity.Y = general.randomFloat(-RangeSpeed, RangeSpeed) + StarSpeed;
                }
            }
            for (int i = 0; i < YellowAm; i++)
            {
                YellowStars[i].UpdateByVelocity();
                if (YellowStars[i].Position.Y > general.HEIGHT + 10)
                {
                    YellowStars[i] = new Entity(ref general, new Vector2(general.RANDOM.Next(0, general.WIDTH), general.RANDOM.Next(-10, 0)), 0.0f, "star/star_yellow",
                        general.randomFloat(general.SCALE * 0.5f, general.SCALE * 1.5f), Layer);
                    YellowStars[i].Velocity.Y = general.randomFloat(-RangeSpeed, RangeSpeed) + StarSpeed;
                }
            }
            for (int i = 0; i < BlueAm; i++)
            {
                BlueStars[i].UpdateByVelocity();
                if (BlueStars[i].Position.Y > general.HEIGHT + 10)
                {
                    BlueStars[i] = new Entity(ref general, new Vector2(general.RANDOM.Next(0, general.WIDTH), general.RANDOM.Next(-10, 0)), 0.0f, "star/star_blue",
                        general.randomFloat(general.SCALE * 0.5f, general.SCALE * 1.5f), Layer);
                    BlueStars[i].Velocity.Y = general.randomFloat(-RangeSpeed, RangeSpeed) + StarSpeed;
                }
            }
        }

        public void DrawAll(ref General general)
        {
            foreach (var entity in WhiteStars)
                entity.DrawEntity(ref general);

            foreach (var entity in RedStars)
                entity.DrawEntity(ref general);

            foreach (var entity in YellowStars)
                entity.DrawEntity(ref general);

            foreach (var entity in BlueStars)
                entity.DrawEntity(ref general);
        }
    }
}
