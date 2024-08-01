using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace SpaceInvaderPlusPlus.Utilities
{
    internal class SpaceBackground
    {
        private float StarSpeed;
        private float OtherSpeed;
        private float RangeSpeed;
        private Entity[][] Stars;
        private Entity[][] Others;
        private int[] AmStars;
        private int[] AmOthers;
        private Texture2D[] NameStars;
        private Texture2D[] NameOthers;
        private float[][] TorqueOthers;
        private float StarLayer;
        private float OtherLayer;

        private List<Entity> ProgressEnt;
        private float ProgEntSpeed;

        public SpaceBackground(ref General general)
        {
            StarSpeed = 0.3f;
            OtherSpeed = 0.6f;
            RangeSpeed = 0.05f;
            StarLayer = 0.1f;
            OtherLayer = 0.11f;

            NameStars = new Texture2D[] { general.ASSETLIBRARY.tStar_w, general.ASSETLIBRARY.tStar_r, general.ASSETLIBRARY.tStar_y, general.ASSETLIBRARY.tStar_b };
            NameOthers = new Texture2D[] { general.ASSETLIBRARY.tOther_wreck, general.ASSETLIBRARY.tOther_bg_rock };
            AmStars = new int[] { 900, 20, 70, 250 };
            AmOthers = new int[] { 4, 7 };
            Stars = new Entity[AmStars.Length][];
            Others = new Entity[AmOthers.Length][];
            TorqueOthers = new float[AmOthers.Length][];
            for (int i = 0; i < AmStars.Length; i++)
                Stars[i] = new Entity[AmStars[i]];
            for (int i = 0; i < AmOthers.Length; i++)
            {
                Others[i] = new Entity[AmOthers[i]];
                TorqueOthers[i] = new float[AmOthers[i]];
            }

            ProgressEnt = new List<Entity>();
            ProgEntSpeed = 0.25f;


            for (int j = 0; j < AmStars.Length; j++)
            {
                for (int i = 0; i < AmStars[j]; i++)
                {
                    Stars[j][i] = new Entity(ref general, new Vector2(general.RANDOM.Next(0, general.WIDTH), general.RANDOM.Next(-10, general.HEIGHT)), 0.0f, NameStars[j],
                    general.randomFloat(general.SCALE * 0.5f, general.SCALE * 1.5f), StarLayer);
                    Stars[j][i].Velocity.Y = general.randomFloat(-RangeSpeed, RangeSpeed) + StarSpeed;
                }
            }
            for (int j = 0; j < AmOthers.Length; j++)
            {
                for (int i = 0; i < AmOthers[j]; i++)
                {
                    Others[j][i] = new Entity(ref general, new Vector2(general.RANDOM.Next(0, general.WIDTH), general.RANDOM.Next(-general.HEIGHT, -100)), 0.0f, NameOthers[j],
                    general.randomFloat(general.SCALE * 0.4f, general.SCALE * 0.8f), OtherLayer);
                    Others[j][i].Velocity.Y = general.randomFloat(-RangeSpeed, RangeSpeed) + OtherSpeed;
                    TorqueOthers[j][i] = general.randomFloat(-0.01f, 0.01f);
                }
            }
        }


        public void Update(ref General general)
        {
            for (int j = 0; j < AmStars.Length; j++)
            {
                for (int i = 0; i < AmStars[j]; i++)
                {
                    if (Stars[j][i].Position.Y > general.HEIGHT + 10)
                    {
                        Stars[j][i] = new Entity(ref general, new Vector2(general.RANDOM.Next(0, general.WIDTH), -10), 0.0f, NameStars[j],
                        general.randomFloat(general.SCALE * 0.5f, general.SCALE * 1.5f), StarLayer);
                        Stars[j][i].Velocity.Y = general.randomFloat(-RangeSpeed, RangeSpeed) + StarSpeed;
                    }
                    Stars[j][i].UpdateByVelocity();
                }
            }
            for (int j = 0; j < AmOthers.Length; j++)
            {
                for (int i = 0; i < AmOthers[j]; i++)
                {
                    if (Others[j][i].Position.Y > general.HEIGHT + 100)
                    {
                        Others[j][i] = new Entity(ref general, new Vector2(general.RANDOM.Next(0, general.WIDTH), general.RANDOM.Next(-general.HEIGHT, -100)), 0.0f, NameOthers[j],
                        general.randomFloat(general.SCALE * 0.4f, general.SCALE * 0.8f), OtherLayer);
                        Others[j][i].Velocity.Y = general.randomFloat(-RangeSpeed, RangeSpeed) + OtherSpeed;
                        TorqueOthers[j][i] = general.randomFloat(-0.01f, 0.01f);
                    }
                    Others[j][i].UpdateByVelocity();
                    Others[j][i].Angle += TorqueOthers[j][i];
                }
            }

            int amount = (int)(general.SCORE_TRAVEL / 500);
            if (amount == 0 && ProgressEnt.Count > amount)
            {
                ProgressEnt.Clear();
            }
            for (int i = 0; i < ProgressEnt.Count; i++)
            {
                ProgressEnt[i].Position.Y += ProgEntSpeed;
                if (ProgressEnt[i].Position.Y > general.HEIGHT + 100)
                {
                    ProgressEnt.RemoveAt(i);
                    i--;
                }
            }
            while (ProgressEnt.Count < amount)
                ProgressEnt.Add(new Entity(ref general, new Vector2(general.RANDOM.Next(0, general.WIDTH), general.RANDOM.Next(-10, general.HEIGHT)),
                    general.randomFloat(-0.2f, 0.2f), general.ASSETLIBRARY.tOther_gaze, general.randomFloat(general.SCALE * 0.5f, general.SCALE * 1.5f), 0.01f));
        }

        public void DrawAll(ref General general)
        {
            for (int j = 0; j < AmStars.Length; j++)
                for (int i = 0; i < AmStars[j]; i++)
                    Stars[j][i].DrawEntity(ref general);

            for (int j = 0; j < AmOthers.Length; j++)
                for (int i = 0; i < AmOthers[j]; i++)
                    Others[j][i].DrawEntity(ref general);

            foreach (var entity in ProgressEnt)
                entity.DrawEntity(ref general);
        }
    }
}
