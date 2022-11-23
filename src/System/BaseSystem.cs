using System.ComponentModel.DataAnnotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TestMonogame.Entities.@abstract;

namespace TestMonogame.System
{
    internal abstract class BaseSystem
    {
        public abstract int ID { get; init; }
        public abstract void Initialize();
        public virtual void Update(GameTime gameTime){}
        public virtual void Draw(SpriteBatch batch){}
        public abstract void AddEntity(BaseEntity entity);
        public virtual void AddEntity<T>() where  T : BaseEntity {}
        public abstract void RemoveEntity(BaseEntity entity);
        public abstract BaseEntity GetEntity(int iD);
    }
}