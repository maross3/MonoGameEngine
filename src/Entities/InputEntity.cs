using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TestMonogame.Entities.@abstract;
using NotImplementedException = System.NotImplementedException;

namespace TestMonogame.Entities
{
    internal class InputEntity : BaseEntity
    {
        public override int ID { get; set; }
        public KeyboardState KeyboardState { get; private set; }
        public MouseState MouseState { get; private set; }

        public bool GetKeyDown(Keys key) =>
            KeyboardState.IsKeyDown(key);

        public void UpdateInput(KeyboardState keys, MouseState mouse)
        {
            KeyboardState = keys;
            MouseState = mouse;
        }
    }
}