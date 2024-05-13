using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Numerics;

namespace SpaceInvaderPlusPlus
{
    class Holder
    {
        public static SpriteBatch spriteBatch { get; set; }
        public static ContentManager content { get; set; }
        public static KeyboardState kState { get; set; }
        public static Random random { get; set; }
        public static float scale { get; set; }
        public static int width { get; set; }
        public static int height { get; set; }
        public static float score { get; set; }
        public static float difMultiplyier { get; set;}
        public static SpriteFont font { get; set; }
    }
}
