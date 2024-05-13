using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaderPlusPlus
{
    public abstract class Environment
    {
        public Vector2 Position { get; set; }

        public abstract void Move(Vector2 direction);
        public abstract void CheckCollisions();
    }
}
