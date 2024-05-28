using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceInvaderPlusPlus.Utilities;
using System;

namespace SpaceInvaderPlusPlus
{
    class Holder
    {
        public static string PLAYERNAME { get; set; }
        public static SpriteBatch SPRITE_BATCH { get; set; }
        public static ContentManager CONTENT { get; set; }
        public static KeyboardState KSTATE { get; set; }
        public static KeyboardState KSTATE_PREV { get; set; }
        public static MouseState MSTATE { get; set; }
        public static MouseState MSTATE_PREV { get; set; }

        public static Random RANDOM { get; set; }

        public static float SCALE { get; set; }
        public static int WIDTH { get; set; }
        public static int HEIGHT { get; set; }

        public static float SCORE_TRAVEL { get; set; }
        public static float SCORE_PICKUPS { get; set; }
        public static float SCORE_DMG { get; set; }
        public static float SCORE_DMGPLAYER { get; set; }
        public static float SCORE_AMMOWASTE { get; set; }
        public static float SCORE_MULTIPLAYER { get; set; }

        public static SpriteFont FONT { get; set; }
        public static bool STARTNEW { get; set; }
        public static int MENUMODE { get; set; }
        public static bool RUNWORLD { get; set; }
        public static Settings SETTINGS { get; set; }
        public static TopPlayers TOP_PLAYERS { get; set; }

        public static float randomFloat(float min = 0, float max = 1)
        {
            return (float)(RANDOM.NextDouble() * (max - min) + min);
        }
    }
}
