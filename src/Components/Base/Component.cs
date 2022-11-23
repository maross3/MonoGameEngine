using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TestMonogame.Components.Base
{
    internal  abstract class Component
    {
        public abstract void Update(GameTime gameTime);
        public virtual void Destroy() {}
        public abstract void Draw(SpriteBatch batch);
    }
}
