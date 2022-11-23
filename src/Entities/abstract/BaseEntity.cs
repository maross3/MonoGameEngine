using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TestMonogame.Entities.@abstract
{
    internal abstract class BaseEntity
    {
        public abstract int ID { get; set; }
        public virtual void Initialize(){}
        public virtual void Initialize(object[] param){}
        public virtual void Update(GameTime gameTime){}
        public virtual void Draw(SpriteBatch batch){}
    }

    internal static class EntityFactory
    {
        private static int curId;
        public static BaseEntity CreateEntity<T>()
        {
            var obj = Activator.CreateInstance(typeof(T));
            if (obj is BaseEntity ent)
            {

                ent.ID = ++curId;
                ent.Initialize();
                return ent;
            }
            return null;
        }

        // todo, create a feed paramteres for constructor (initialize)
        public static BaseEntity CreateEntity<T>(params object[] param) where T : BaseEntity
        {
            var obj = Assembly.GetCallingAssembly().CreateInstance(nameof(T));
            if (obj is BaseEntity ent)
            {
                ent.ID = ++curId;
                ent.Initialize(param);
                return ent;
            }
            return null;
        }
    }
}