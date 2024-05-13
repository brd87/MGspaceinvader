using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaderPlusPlus
{
    public class Entity
    {
        public Vector2 Position;
        public Vector2 Diameters { get; set; }
        public float Angle { get; set; }
        public Texture2D EntityTexture { get; set; }
        public int EntityLayer { get; set; }
        public bool CollisionMark { get; set; }

        public Entity(Vector2 position, float angle, string spriteName, int entityLayer)
        {
            Position = position;
            Angle = angle;
            EntityTexture = Holder.content.Load<Texture2D>(spriteName);
            EntityLayer = entityLayer;
            CollisionMark = false;
        }

        public void UpdateSprite(string spriteName)
        {
            EntityTexture = Holder.content.Load<Texture2D>(spriteName);
        }

        public void DrawEntity()
        {
            if(EntityTexture != null)
            {
                Holder.spriteBatch.Draw(EntityTexture, Position, null,
                    Color.White, Angle, new Vector2(EntityTexture.Bounds.Width/2, EntityTexture.Bounds.Height/2), Holder.scale, SpriteEffects.None, EntityLayer);
            }
        }
    }
}
