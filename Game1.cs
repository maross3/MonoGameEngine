using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TestMonogame.Components;
using TestMonogame.Data;
using TestMonogame.System;

namespace TestMonogame
{
    public class Game1 : Game
    {
        // abstrct
        private GraphicsDeviceManager _graphics;

        public static Rectangle GameWindowBounds { get; set; }
        private SpriteBatch _spriteBatch;

        // todo make font helper class
        public static SpriteFont electroFontOne;

        // todo texture helper class
        public static Texture2D treeOne;
        public static Texture2D pixelFloorTest;
        public static Texture2D windowTexture;
        public static Texture2D progressBarOneFront;
        public static Texture2D progressBarOneBack;

        //  db
        public static SqlManager database;

        // systems
        private List<BaseSystem> _systems;
        internal static IdleSystem idleSystem;
        internal static InputSystem inputSystem;
        internal static AnimationSystem animationSystem;
        internal static EnvironmentSystem environmentSystem;

        internal static Player player;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        private void CreateIdleSystem()
        {
            idleSystem = new IdleSystem();
            idleSystem.Initialize();
            _systems.Add(idleSystem);
        }

        private void CreateInputSystem()
        {
            inputSystem = new InputSystem(this);
            inputSystem.Initialize();
            _systems.Add(inputSystem);
        }

        private void CreateAnimationSystem()
        {
            animationSystem = new AnimationSystem();
            animationSystem.Initialize();
            _systems.Add(animationSystem);
        }

        private void CreateEnvironment()
        {
            environmentSystem = new EnvironmentSystem();
            environmentSystem.Initialize();
            _systems.Add(environmentSystem);
        }

        protected override void Initialize()
        {
            GameWindowBounds = Window.ClientBounds;
            database = new SqlManager();
            _systems = new List<BaseSystem>();

            CreateInputSystem();
            base.Initialize();
            CreateIdleSystem();
            CreateEnvironment();
            CreateAnimationSystem();
            player = new Player(
                AnimationDictionaryBuilder.BuildPlayerDictionary());
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            windowTexture = Content.Load<Texture2D>("Panel 3_");
            electroFontOne = Content.Load<SpriteFont>("ElectroFont");
            treeOne = Content.Load<Texture2D>("treePngTransparent");
            progressBarOneFront = Content.Load<Texture2D>("./ProgressBar Light Blue/V1/Background Bar");
            progressBarOneBack = Content.Load<Texture2D>("./ProgressBar Light Blue/V1/Foreground");
            pixelFloorTest = Content.Load<Texture2D>("pixelFloorTest");
            
            AnimationDictionaryBuilder.LoadPlayerFrames(this);
        }

        protected override void Update(GameTime gameTime)
        {
            foreach (var sys in _systems) sys.Update(gameTime);
            player.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // need an environment drawing system
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            foreach (var sys in _systems) sys.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}