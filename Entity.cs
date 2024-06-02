using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaderPlusPlus
{
    public class Entity
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public float Angle { get; set; }
        public Texture2D EntityTexture { get; set; }
        public float EntityLayer { get; set; }
        public bool CollisionMark { get; set; }
        public float Scale { get; set; }

        public Entity(ref General general, Vector2 position, float angle, string spriteName = null, float? scale = null, float entityLayer = 1)
        {
            Position = position;
            Velocity = Vector2.Zero;

            Angle = angle;
            if (spriteName != null)
                EntityTexture = general.CONTENT.Load<Texture2D>(spriteName);
            else
                EntityTexture = general.CONTENT.Load<Texture2D>("other/error");
            EntityLayer = entityLayer;
            CollisionMark = false;
            Scale = scale ?? general.SCALE;
        }

        public void UpdateSprite(ref General general, string spriteName)
        {
            EntityTexture = general.CONTENT.Load<Texture2D>(spriteName);
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
