using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TestMonogame.Components.@abstract;
using TestMonogame.Math;

namespace TestMonogame.Components
{
    internal class ProgressBar : GameObject
    {
        public override string Name { get; init; }
        public override Texture2D Texture { get; init; }
        public float Value { get; set; } = 1f;

        private Vector2 ScaleVector => new(Value + 0.05f, 1);
        private readonly Vector2 _xOffset = new() { X = 3, Y = 1 };

        public ProgressBar(Vector2 position, Vector2 scale, MonoTransform parent)
        {
            Transform.Position = position;
            Transform.Scale = scale;
            Transform.Parent = parent;
        }

        public override int ID { get; set; }

        public override void Update(GameTime gameTime)
        {
        }


        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(
                Game1.progressBarOneFront,
                Transform.Position + _xOffset,
                null,
                Color.White,
                0f,
                new Vector2(0, 0),
                Transform.Scale * ScaleVector,
                SpriteEffects.None,
                0f
            );

            batch.Draw(
                Game1.progressBarOneBack,
                Transform.Position,
                null,
                Color.White,
                0f,
                new Vector2(0, 0),
                Transform.Scale,
                SpriteEffects.None,
                0f
            );
        }
    }
}