using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TestMonogame.Components;
using TestMonogame.Entities.@abstract;
using TestMonogame.Math;

namespace TestMonogame.Entities
{
    // todo, abstract out animations to allow state setting by using <T>
    // AnimationEntity<PlayerAnimationState>
    internal class AnimationEntity<T> : BaseEntity
    {
        public override int ID { get; set; }
        protected Dictionary<T, Texture2D[]> lookup;
        protected T currentState;

        public TimeSpan frameDelay = TimeSpan.FromMilliseconds(200);
        protected MonoTransform Transform { get; set; }
        protected int frame;

        private float _targetWaitMs;
        private protected int frameMax;

        public AnimationEntity(MonoTransform objTransform)
        {
            Transform = objTransform;
        }

        public void UpdateFrameSpeed(float newSpeed)
        {
            frameDelay = TimeSpan.FromMilliseconds(newSpeed);
        }

        public void SetState(T state)
        {
            if (Compare(state, currentState))
                return;

            currentState = state;
            frameMax = lookup[state].Length - 1;
            frame = 0;
            _targetWaitMs = 0;
        }

        public T GetState() => currentState;

        private static bool Compare(T x, T y)
        {
            return EqualityComparer<T>.Default.Equals(x, y);
        }

        public override void Update(GameTime gameTime)
        {
            var lastMs = (float)gameTime.TotalGameTime.TotalMilliseconds;
            if (_targetWaitMs - lastMs > 0) return;
            _targetWaitMs = lastMs + (float)frameDelay.TotalMilliseconds;
            AdvanceFrame();
        }

        private void AdvanceFrame()
        {
            if (frame < frameMax) frame++;
            else frame = 0;
        }

        private SpriteEffects _spriteEffect = SpriteEffects.None;

        public void DrawHorizontalFlipped(bool moveLeft) =>
            _spriteEffect = moveLeft ? SpriteEffects.FlipHorizontally : SpriteEffects.None;


        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(
                lookup[currentState][frame],
                Transform.Position,
                null,
                Color.White,
                0f,
                new Vector2(0, 0),
                Transform.Scale,
                _spriteEffect,
                0f
            );
        }
    }
}