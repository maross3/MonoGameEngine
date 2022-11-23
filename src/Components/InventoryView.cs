using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TestMonogame.Components.@abstract;
using TestMonogame.Entities;
using TestMonogame.Entities.@abstract;
using Vector2 = Microsoft.Xna.Framework.Vector2;

// todo fix transform
// allow position.Y += 4?
// abstracted out to move position (replace with translate?)

namespace TestMonogame.Components
{
    internal class InventoryView : GameObject, IIdleImplemented
    {
        public override int ID { get; set; }
        public override string Name { get; init; }
        public sealed override Texture2D Texture { get; init; }

        const float _speed = 30;
        private readonly SpriteFont _font;

        public int LastKnowntime { get; set; }
        public Button saveButton;
        public Button chopWoodButton;
        public ProgressBar progressBarOne;
        private InputEntity _input;

        public InventoryView(Texture2D tex, SpriteFont font) : base()
        {
            _font = font;
            Texture = tex;
            Transform.Position = new Vector2(17, 49);
            Transform.Scale = new Vector2(0.5f);
        }


        public override  void Initialize()
        {
            CreateSaveButton();
            CreateWoodButton();
            CreateProgressBar();
            InitializeInputEntity();
        }

        private void InitializeInputEntity()
        {
            _input = new InputEntity();
            Game1.inputSystem.AddEntity(_input);
        }

        private void CreateProgressBar()
        {
            var progressBarOnePosition = new Vector2(Width * 0.09f, Height * 0.6f);
            progressBarOne = new ProgressBar(progressBarOnePosition, new Vector2(0.7f), Transform);
        }

        private void CreateWoodButton()
        {
            var chopWoodPosition = new Vector2(Width * 0.07f, Height * 0.75f);

            chopWoodButton = new Button(chopWoodPosition, new Vector2(0.2f, 0.1f), "chop\nwood", Transform)
                { Texture = Texture, Name = "chopWoodButton" };
        }

        private void CreateSaveButton()
        {
            var saveGamePosition = new Vector2(Width * 0.53f, Height * 0.75f);

            saveButton = new Button(saveGamePosition, new Vector2(0.2f, 0.1f), "save\ngame", Transform)
                { Texture = Texture, Name = "saveGameButton" };
        }

        public override void Update(GameTime gameTime)
        {
            saveButton.Update(gameTime);
            chopWoodButton.Update(gameTime);

            var time = (float)gameTime.ElapsedGameTime.TotalSeconds;
            MoveWindow(time);
        }

        private void MoveWindow(float time)
        {
            if (_input.GetKeyDown(Keys.Up)) Transform.MoveUpBy(time * _speed);
            if (_input.GetKeyDown(Keys.Down)) Transform.MoveDownBy(time * _speed);
            if (_input.GetKeyDown(Keys.Left)) Transform.MoveLeftBy(time * _speed);
            if (_input.GetKeyDown(Keys.Right)) Transform.MoveRightBy(time * _speed);
        }

        public void Accept(object obj)
        {
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

            saveButton.Draw(batch);
            chopWoodButton.Draw(batch);
            progressBarOne.Draw(batch);
        }

        public void AdvanceTime(float value) =>
            progressBarOne.Value = value;

        public void ApplySecondDifference(int secondDifference)
        {
        }
    }
}