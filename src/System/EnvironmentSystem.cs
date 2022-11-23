using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TestMonogame.Entities;
using TestMonogame.Entities.@abstract;
using NotImplementedException = System.NotImplementedException;

namespace TestMonogame.System
{
    internal class EnvironmentSystem : BaseSystem
    {
        private readonly Dictionary<int, EnvironmentEntity> 
            _environmentEntities = new();
        public override int ID { get; init; }
        
        
        public override void Draw(SpriteBatch batch)
        {
            foreach(var entity in _environmentEntities.Values) 
                entity.Draw(batch);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var entity in _environmentEntities.Values)
                entity.Update(gameTime);
        }

        public override void Initialize()
        {
            AddEntity<EnvironmentEntity>();
        }

        public override void AddEntity(BaseEntity entity)
        {
            _environmentEntities.Add(entity.ID, entity as EnvironmentEntity);
        }

        public override void RemoveEntity(BaseEntity entity) =>
            _environmentEntities.Remove(entity.ID);

        public override BaseEntity GetEntity(int iD) =>
            _environmentEntities[iD];

        public override void AddEntity<T>() =>
            AddEntity(EntityFactory.CreateEntity<T>());
    }
}