using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaderPlusPlus
{
    public class Entity
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public float Angle { get; set; }
        public Texture2D EntityTexture { get; set; }
        public int EntityLayer { get; set; }
        public bool CollisionMark { get; set; }
        public bool CollisionHardMark { get; set; }
        public float Scale { get; set; }

        public Entity(Vector2 position, float angle, string spriteName, int entityLayer, float scale = -1)//, ContentManager content = null)
        {
            Position = position;
            Velocity = Vector2.Zero;

            Angle = angle;
            EntityTexture = Holder.CONTENT.Load<Texture2D>(spriteName);
            /*
            if(content == null)
                EntityTexture = Holder.CONTENT.Load<Texture2D>(spriteName);
            else
                EntityTexture = content.Load<Texture2D>(spriteName);
            */
            EntityLayer = entityLayer;
            CollisionMark = false;
            CollisionHardMark = false;
            if (scale == -1)
                Scale = Holder.SCALE;
            else
                Scale = scale;
        }

        public void UpdateSprite(string spriteName)
        {
            EntityTexture = Holder.CONTENT.Load<Texture2D>(spriteName);
        }

        public void UpdateByVelocity()
        {
            Position += Velocity;
        }

        public void DrawEntity()
        {
            if (EntityTexture != null)
            {
                Holder.SPRITE_BATCH.Draw(EntityTexture, Position, null,
                    Color.White, Angle, new Vector2(EntityTexture.Bounds.Width / 2, EntityTexture.Bounds.Height / 2), Scale, SpriteEffects.None, EntityLayer);
            }
        }
    }
}
