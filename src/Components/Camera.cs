using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using TestMonogame.Components.@abstract;
using TestMonogame.Math;

namespace TestMonogame.src.Components
{
    internal class Camera : GameObject
    {
        public override int ID { get; set; }
        public override string Name { get; init; }
        public override Texture2D Texture { get; init; }

        public MonoTransform focus => _focusTarget;
        private MonoTransform _focusTarget;
        private float _zoom;
        private Size cameraBounds;
        public Camera(MonoTransform focus, float zoom)
        {
            _focusTarget = focus;
            _zoom = zoom;
        }

        public void ChangeFocusTarget(MonoTransform focusTarget) =>
            _focusTarget = focusTarget;
        
    }
}
