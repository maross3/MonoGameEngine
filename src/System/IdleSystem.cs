using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TestMonogame.Entities;
using TestMonogame.Entities.@abstract;
using NotImplementedException = System.NotImplementedException;

namespace TestMonogame.System
{
    internal class IdleSystem : BaseSystem
    {
        private readonly Dictionary<int, IIdleImplemented> 
            _idleSystemEntities = new();
        
        public override int ID { get; init; }

        public override void Initialize() => 
            AddEntity<IdleEntity>();

        public override void Update(GameTime gameTime)
        {
            foreach (var ent in _idleSystemEntities.Values)
                if (ent is BaseEntity entity) entity.Update(gameTime);
        }

        public override void Draw(SpriteBatch batch)
        {
            foreach (var ent in _idleSystemEntities.Values)
                if (ent is BaseEntity entity) entity.Draw(batch);
        }

        public override void AddEntity<T>() =>
            AddEntity(EntityFactory.CreateEntity<T>());

        public override void AddEntity(BaseEntity baseEntity)
        {
            if (DoesImplementIdleSystem(baseEntity, out var entity))
                _idleSystemEntities.Add(baseEntity.ID, entity);
        }

        public override void RemoveEntity(BaseEntity baseEntity)
        {
            if (DoesImplementIdleSystem(baseEntity, out var entity) 
                && _idleSystemEntities.ContainsKey(baseEntity.ID))
                _idleSystemEntities.Remove(baseEntity.ID);
        }

        public override BaseEntity GetEntity(int iD) =>
            _idleSystemEntities[iD] as BaseEntity;

        private static bool DoesImplementIdleSystem(BaseEntity baseEntity, out IIdleImplemented entity)
        {
            entity = null;
            if (baseEntity is IIdleImplemented ent)
                entity = ent;

            return entity != null;
        }
    }
}
