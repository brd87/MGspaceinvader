using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceInvaderPlusPlus.Utilities;
using System;

namespace SpaceInvaderPlusPlus
{
    public class General
    {
        public string PLAYERNAME { get; set; }
        public SpriteBatch SPRITE_BATCH { get; set; }
        public ContentManager CONTENT { get; set; }
        public KeyboardState KSTATE { get; set; }
        public KeyboardState KSTATE_PREV { get; set; }

        public Random RANDOM { get; set; }

        public float SCALE { get; set; }
        public int WIDTH { get; set; }
        public int HEIGHT { get; set; }

        public float SCORE_TRAVEL { get; set; }
        public float SCORE_PICKUPS { get; set; }
        public float SCORE_DMG { get; set; }
        public float SCORE_DMGPLAYER { get; set; }
        public float SCORE_AMMOWASTE { get; set; }
        public float SCORE_MULTIPLAYER { get; set; }

        public SpriteFont FONT { get; set; }
        public bool STARTNEW { get; set; }
        public int MENUMODE { get; set; }
        public bool RUNWORLD { get; set; }
        public Settings SETTINGS { get; set; }
        public TopPlayers TOP_PLAYERS { get; set; }

        public float randomFloat(float min = 0, float max = 1)
        {
            return (float)(RANDOM.NextDouble() * (max - min) + min);
        }
    }
}
