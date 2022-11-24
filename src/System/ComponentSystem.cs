using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TestMonogame.Entities.@abstract;
using TestMonogame.System;

namespace TestMonogame.src.System
{
    internal class ComponentSystem : BaseSystem
    {
        public override int ID { get; init; }

        public Dictionary<int, BaseEntity> components = new();

        public override void AddEntity(BaseEntity entity)
        {
            if (components.ContainsKey(entity.ID)) return;
            components.Add(entity.ID, entity);
        }

        public override BaseEntity GetEntity(int iD) =>
            components[iD];


        public override void Initialize()
        {
        }

        public override void RemoveEntity(BaseEntity entity)
        {
            if (!components.ContainsKey(entity.ID)) return;
            components.Remove(entity.ID);
        }

        public override void Draw(SpriteBatch batch)
        {
            foreach (var component in components)
                component.Value.Draw(batch);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in components)
                component.Value.Update(gameTime);
        }
    }
}