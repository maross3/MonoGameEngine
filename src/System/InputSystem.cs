using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TestMonogame.Entities;
using TestMonogame.Entities.@abstract;
using NotImplementedException = System.NotImplementedException;

namespace TestMonogame.System
{
    internal class InputSystem : BaseSystem
    {
        private Game1 _game;

        public InputSystem(Game1 game)
        {
            _game = game;
        }

        private readonly Dictionary<int, InputEntity>
            _systemEntities = new();

        public override int ID { get; init; }

        public override void Initialize() =>
            AddEntity<InputEntity>();

        public override void AddEntity(BaseEntity entity) =>
            _systemEntities.Add(entity.ID, entity as InputEntity);

        public override void RemoveEntity(BaseEntity entity) =>
            _systemEntities.Remove(entity.ID);

        public override BaseEntity GetEntity(int iD) =>
            _systemEntities[iD];


        public override void Update(GameTime gameTime)
        {
            var keys = Keyboard.GetState();

            // gamepad support?
            //  if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            if (keys.IsKeyDown(Keys.Escape)) _game.Exit();

            var mouse = Mouse.GetState();

            foreach (var ent in _systemEntities)
                ent.Value.UpdateInput(keys, mouse);
        }

        public override void AddEntity<T>() =>
            AddEntity(EntityFactory.CreateEntity<T>());

    }
}