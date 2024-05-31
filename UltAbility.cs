﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace SpaceInvaderPlusPlus
{
    public class UltAbility : Entity
    {
        public int Damage { get; set; }
        private float Acceleration { get; set; }
        public bool Done { get; set; }
        private SoundEffectInstance UltSoundEffectIns { get; set; }
        public UltAbility(Vector2? position = null, float angle = 0.0f, string spriteName = "other/ult_shock", int entityLayer = 1)
            : base(position ?? new Vector2(Holder.WIDTH / 2, Holder.HEIGHT + 100), angle, spriteName, entityLayer)
        {
            Damage = 15;
            Acceleration = 2;
            Done = false;
            UltSoundEffectIns = Holder.CONTENT.Load<SoundEffect>("eff/eff_select").CreateInstance();
            UltSoundEffectIns.Volume = Holder.SETTINGS.LastEffectsVolume;
            UltSoundEffectIns.Play();
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

            Velocity.Y -= Acceleration;
            for (int i = 0; i < Enemies.Count; i++)
                for (int j = 0; j < Enemies[i].Count; j++)
                {
                    if (!Enemies[i][j].UltRecived)
                        if (this.Position.Y - Enemies[i][j].Position.Y < Enemies[i][j].EntityTexture.Height / 2 + this.EntityTexture.Height / 2)
                        {
                            Enemies[i][j].Health -= Damage;
                            Enemies[i][j].Velocity.Y = -3;
                            Enemies[i][j].UltRecived = true;
                        }
                }
            UpdateByVelocity();
        }

    }
}
