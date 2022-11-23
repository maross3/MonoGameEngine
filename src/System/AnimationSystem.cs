using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TestMonogame.Entities.@abstract;
using NotImplementedException = System.NotImplementedException;

namespace TestMonogame.System
{
    internal class AnimationSystem : BaseSystem
    {
        public override int ID { get; init; }


        private readonly Dictionary<int, BaseEntity>
            _animationSystemEntities = new();

        public override void Initialize()
        {
        }

        public override void Draw(SpriteBatch batch)
        {
            foreach(var animationEntity
                    in _animationSystemEntities)
                animationEntity.Value.Draw(batch);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var animationEntity in _animationSystemEntities)
                animationEntity.Value.Update(gameTime);
        }

        public override void AddEntity<T>() =>
            AddEntity(EntityFactory.CreateEntity<T>());

        public override void AddEntity(BaseEntity entity)
        {
            _animationSystemEntities.Add(entity.ID, entity);
        }

        public override void RemoveEntity(BaseEntity entity) =>
            _animationSystemEntities.Remove(entity.ID);

        public override BaseEntity GetEntity(int iD) =>
            _animationSystemEntities[iD];
        }
}