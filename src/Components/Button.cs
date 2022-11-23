using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TestMonogame.Components.@abstract;
using TestMonogame.Math;
using TestMonogame.System;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace TestMonogame.Components
{
    internal class Button : GameObject
    {
        private TimeSpan _msDelayOnPress;
        private static readonly TimeSpan ButtonDelayMs = TimeSpan.FromMilliseconds(260);
        private bool _pressed;
        
        
        public event EventHandler<EventArgs> OnButtonPress;
        public string Text { get; set; } = "";
        public Vector2 textOffset;
        public override string Name { get; init; }
        public override Texture2D Texture { get; init; }

        public Vector2 TextOffset
        {
            get => textOffset;
            set => textOffset = new Vector2(value.X + 22, value.Y + 10);
        }

        private Color _curColor;
        // should not be read only in product
        private readonly Color _highlightColor = Color.White;
        private readonly Color _defaultColor = Color.LightGray;
        private Color _disabledColor = Color.Gray;

        #region ctor overloads
        public Button(Vector2 position)
        {
            Transform.Position = position;
        }

        public Button(Vector2 position, Vector2 scale)
        {
            Transform.Position = position;
            Transform.Scale = scale;
        }

        public Button(Vector2 position, Vector2 scale, string text)
        {
            Transform.Position = position;
            TextOffset = Transform.Position;

            Transform.Scale = scale;
            Text = text;
        }

        /// <summary>
        /// Set position of button in local space relative to passed parent.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="scale"></param>
        /// <param name="text"></param>
        /// <param name="parent"></param>
        public Button(Vector2 position, Vector2 scale, string text, MonoTransform parent)
        {
            _curColor = _defaultColor;
            Transform.Position = position;
            TextOffset = Transform.Position;

            Transform.Scale = scale;
            Text = text;
            Transform.Parent = parent;
        }
        public Button(Vector2 position, Vector2 scale, string text, MonoTransform parent, InputSystem inputSys)
        {
            _curColor = _defaultColor;
            Transform.Position = position;
            TextOffset = Transform.Position;

            Transform.Scale = scale;
            Text = text;
            Transform.Parent = parent;
        }
        #endregion

        public bool MouseHover() =>
            Mouse.GetState().X < Transform.Position.X + Width &&
            Mouse.GetState().X > Transform.Position.X &&
            Mouse.GetState().Y < Transform.Position.Y + Height &&
            Mouse.GetState().Y > Transform.Position.Y;

        public override int ID { get; set; }

        public override void Update(GameTime gameTime)
        {
            TextOffset = Transform.Position;
            var mouseHovering = MouseHover();
            _curColor = mouseHovering ? _highlightColor : _defaultColor;

            if (_pressed) _pressed = _msDelayOnPress + ButtonDelayMs > gameTime.TotalGameTime;
            if (_pressed || !mouseHovering || Mouse.GetState().LeftButton != ButtonState.Pressed || _pressed) return;
            _pressed = true;

            _msDelayOnPress = gameTime.TotalGameTime;
            OnButtonPress?.Invoke(this, EventArgs.Empty);
            Debug.WriteLine("Im pressed!");
            _curColor = _defaultColor;
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(
                Texture,
                Transform.Position,
                null,
                _curColor,
                0f,
                new Vector2(0, 0),
                Transform.Scale,
                SpriteEffects.None,
                0f
            );
            if (Text == "") return;
            batch.DrawString(Game1.electroFontOne,
                Text,
                TextOffset,
                Color.LightGray,
                0,
                Vector2.Zero,
                new Vector2(0.8f),
                SpriteEffects.None,
                0);
        }
    }
}