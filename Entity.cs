using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaderPlusPlus
{
    internal class Entity
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public float Angle { get; set; }
        public Texture2D EntityTexture { get; set; }
        public float EntityLayer { get; set; }
        public bool CollisionMark { get; set; }
        public float Scale { get; set; }

        public Entity(ref General general, Vector2 position, float angle, Texture2D spriteName, float? scale = null, float entityLayer = 0.99f)
        {
            Position = position;
            Velocity = Vector2.Zero;
            Angle = angle;
            EntityTexture = spriteName;
            EntityLayer = entityLayer;
            CollisionMark = false;
            Scale = scale ?? general.SCALE;
        }

        public void UpdateSprite(Texture2D spriteName)
        {
            EntityTexture = spriteName;
        }

        public void UpdateByVelocity()
        {
            Position += Velocity;
        }

        public void DrawEntity(ref General general)
        {
            general.SPRITE_BATCH.Draw(EntityTexture, Position, null,
                    Color.White, Angle, new Vector2(EntityTexture.Bounds.Width / 2, EntityTexture.Bounds.Height / 2), Scale, SpriteEffects.None, EntityLayer);
        }
    }
}
