using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TestMonogame.Entities.@abstract;
using TestMonogame.Math;

namespace TestMonogame.Components.@abstract
{
    internal abstract class GameObject : BaseEntity
    {
        public MonoTransform Transform { get; } = new();
        public abstract string Name { get; init; }
        public abstract Texture2D Texture { get; init; }
        public List<Component> Components { get; } = new();
        public int Height => (int)(Texture.Height * Transform.Scale.Y);

        public int Width =>
            (int)(Texture.Width * Transform.Scale.X);

        public Rectangle Bounds =>
            new((int)Transform.Position.X, (int)Transform.Position.Y, Width, Height);

        public Rectangle LocalBounds =>
            new((int)Transform.LocalPosition.X, (int)Transform.LocalPosition.Y, Width, Height);

        // physics calls this
        public bool Intersect(Vector2 pOne, Vector2 pTwo)
        {
            var dx = pTwo.X - pOne.X;
            var dy = pTwo.Y - pOne.Y;
            float t0 = 0;
            float t1 = 1;

            if (!CheckEdge(Bounds.Left, -dx, pOne.X, ref t1, ref t0, true)) return true;
            if (!CheckEdge(Bounds.Right, dx, pOne.X, ref t1, ref t0, false)) return true;
            if (!CheckEdge(Bounds.Bottom, -dy, pOne.Y, ref t1, ref t0, true)) return true;
            if (!CheckEdge(Bounds.Top, dy, pOne.Y, ref t1, ref t0, false)) return true;
            return false;
        }

        private bool CheckEdge(float edge, float delta, float pZero, ref float t1, ref float t0, bool negative)
        {
            if (delta < 0)
            {
                var q = edge - pZero;
                if (negative) q *= -1;
                var r = q / delta;
                if (delta == 0 && q < 0 || r > t1) return false;
                if (r > t0) t0 = r;
            }
            else if (delta > 0)
            {
                var q = edge - pZero;
                if (negative) q *= -1;
                var r = q / delta;
                if (delta == 0 && q < 0 || r < t0) return false;
                if (r < t1) t1 = r;
            }

            return true;
        }

        public void AddComponent<T>(T component)
        {
            if (component is Component comp) Components.Add(comp);
        }

        public void RemoveComponent<T>(T component)
        {
            if (component is Component comp && Components.Contains(comp))
                Components.Remove(comp);
        }
    }
}