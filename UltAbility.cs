﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaderPlusPlus
{
    public class UltAbility : Entity
    {
        public int Damage { get; set; }
        public float Velocity { get; set; }
        private float Acceleration { get; set; }
        private bool Done { get; set; }
        public UltAbility(Vector2? position = null, float angle = 0.0f, string spriteName = "ultwave", int entityLayer = 1) 
            : base(position ?? new Vector2(Holder.WIDTH / 2, Holder.HEIGHT + 100), angle, spriteName, entityLayer)
        {
            Damage = 100;
            Velocity = 0;
            Done = false;
        }


        public void Update(List<List<Enemy>> Enemies)
        {
            if (this.Position.Y < -100)
            {
                for (int i = 0; i < Enemies.Count; i++)
                    for (int j = 0; j < Enemies[i].Count; j++)
                        Enemies[i][j].UltRecived = false;
                Done = true;
                return;
            }

            Velocity += Acceleration;
            for (int i = 0; i < Enemies.Count; i++)
                for (int j = 0; j < Enemies[i].Count; j++)
                {
                    if(!Enemies[i][j].UltRecived)
                        if (Enemies[i][j].Position.Y - this.Position.Y < Enemies[i][j].EntityTexture.Height / 2 + this.EntityTexture.Height / 2)
                        {
                            Enemies[i][j].Health -= Damage;
                            Enemies[i][j].Velocity.Y /= -3;
                            Enemies[i][j].UltRecived = true;
                        }
                }
            this.Position.Y -= Velocity;
        }

    }
}
