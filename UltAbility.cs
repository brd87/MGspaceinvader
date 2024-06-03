using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace SpaceInvaderPlusPlus
{
    internal class UltAbility
    {
        public Entity UltMain {  get; set; }
        public int Damage { get; set; }
        private float Acceleration { get; set; }
        public bool Done { get; set; }
        private SoundEffectInstance UltSoundEffectIns { get; set; }
        public UltAbility(ref General general)
        {
            UltMain = new Entity(ref general, new Vector2(general.WIDTH / 2, general.HEIGHT + 100), 0.0f, general.ASSETLIBRARY.tOther_ultShock);
            Damage = 15;
            Acceleration = 2;
            Done = false;
            UltSoundEffectIns = general.ASSETLIBRARY.eff_Ult.CreateInstance();
            UltSoundEffectIns.Volume = general.SETTINGS.LastEffectsVolume;
            UltSoundEffectIns.Play();
        }


        public void Update(List<List<Enemy>> Enemies)
        {
            if (UltMain.Position.Y < -100)
            {
                for (int i = 0; i < Enemies.Count; i++)
                    for (int j = 0; j < Enemies[i].Count; j++)
                        Enemies[i][j].UltRecived = false;
                Done = true;
                return;
            }

            UltMain.Velocity.Y -= Acceleration;
            for (int i = 0; i < Enemies.Count; i++)
                for (int j = 0; j < Enemies[i].Count; j++)
                {
                    if (!Enemies[i][j].UltRecived)
                        if (UltMain.Position.Y - Enemies[i][j].EnMain.Position.Y < Enemies[i][j].EnMain.EntityTexture.Height / 2 + UltMain.EntityTexture.Height / 2)
                        {
                            Enemies[i][j].Health -= Damage;
                            Enemies[i][j].EnMain.Velocity.Y = -3;
                            Enemies[i][j].UltRecived = true;
                        }
                }
            UltMain.UpdateByVelocity();
        }

    }
}
