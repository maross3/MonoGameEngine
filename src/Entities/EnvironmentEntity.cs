using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TestMonogame.Components.@abstract;
using TestMonogame.Entities.@abstract;
using TestMonogame.Math;

namespace TestMonogame.Entities
{
    internal class EnvironmentEntity : BaseEntity
    {
        internal class EnvironmentTile : GameObject
        {
            public override int ID { get; set; }

            public override string Name { get; init; }
            public override Texture2D Texture { get; init; }

            public GameObject[] collidables;

            public EnvironmentTile(Vector2 position, Vector2 scale)
            {
                Transform.Position = position;
                Transform.Scale = scale;
            }

            public override void Draw(SpriteBatch batch)
            {
                batch.Draw(
                    Texture,
                    Transform.Position,
                    null,
                    Color.White,
                    0f,
                    new Vector2(0, 0),
                    Transform.Scale,
                    SpriteEffects.None,
                    0f
                );
            }
        }

        public MonoTransform mainEnvironment = new();
        public override int ID { get; set; }
        public EnvironmentTile[,] environmentObjects;

        private readonly Vector2 _offset = new(
            Game1.pixelFloorTest.Width * 0.1f,
            Game1.pixelFloorTest.Height * 0.1f);

        public InputEntity input;

        public override void Initialize()
        {
            environmentObjects = new EnvironmentTile[60, 60];

            for (var x = 0; x < 60; x++)
            for (var y = 0; y < 60; y++)
            {
                environmentObjects[x, y] =
                    new EnvironmentTile(new Vector2(x, y) * _offset, new Vector2(0.1f, 0.1f))
                        { Texture = Game1.pixelFloorTest };

                environmentObjects[x, y].Transform.Parent = mainEnvironment;
            }

            input = EntityFactory.CreateEntity<InputEntity>() as InputEntity;
            Game1.inputSystem.AddEntity(input);

            // todo physics test does not belong here
            /*
            var testPOne = environmentObjects[1, 1].Transform.Position;
            var testPOne2 = environmentObjects[1, 4].Transform.Position;
            Debug.WriteLine(environmentObjects[1, 2].Intersect(testPOne, testPOne2));
            */
            // initialize a grid of game objects
        }

        private float _speed = 60;

        public override void Update(GameTime gameTime)
        {
            var time = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (input.GetKeyDown(Keys.A)) mainEnvironment.MoveRightBy(_speed * time);
            if (input.GetKeyDown(Keys.D)) mainEnvironment.MoveLeftBy(_speed * time);
            if (input.GetKeyDown(Keys.S)) mainEnvironment.MoveUpBy(_speed * time);
            if (input.GetKeyDown(Keys.W)) mainEnvironment.MoveDownBy(_speed * time);
        }

        public override void Draw(SpriteBatch batch)
        {
            for (var x = 0; x < 60; x++)
            for (var y = 0; y < 60; y++)
            {
                environmentObjects[x, y].Draw(batch);
            }
            // draw grid by local space
            // 
        }
    }
}