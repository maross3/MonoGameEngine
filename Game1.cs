using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TestMonogame.Components;
using TestMonogame.Data;
using TestMonogame.src.Components;
using TestMonogame.System;

namespace TestMonogame
{
    // disconnected database because .txt was not found in bin, need to automate that
    // TODO:
    // transfer camera code to component, main camera in scene
    // Make the environment stay still
    // parent player on transform and make sure camera parenting/following works
    // dev path-finding pipeline in environment chunks
    // quad tree environment
    // path finding using quad tree
    // physics
    // building:
    // art for build icon
    // build menu
    // placement system (valid and invalid)
    // pathfind to build site
    // idle while building, move cancels

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

        public SpriteBatch targetBatch;
        private RenderTarget2D _target;
        public static float CameraScale = 1f;
        private static float maxZoomScale = 2f;
        private static float minZoomScale = 1f;



        protected override void Initialize()
        {
            targetBatch = new SpriteBatch(GraphicsDevice);

            _target = new RenderTarget2D(GraphicsDevice,
                (int)(GraphicsDevice.DisplayMode.Width * (maxZoomScale * CameraScale)),
                (int)(GraphicsDevice.DisplayMode.Height * (maxZoomScale * CameraScale)));


            GameWindowBounds = new Rectangle(0,0, 
                GraphicsDevice.Viewport.Width * (int)(maxZoomScale * CameraScale), 
                GraphicsDevice.Viewport.Height * (int)(maxZoomScale * CameraScale));

            // GameWindowBounds = Window.ClientBounds;
            // database = new SqlManager();
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
            if (Keyboard.GetState().IsKeyDown(Keys.N) && CameraScale > minZoomScale) 
                CameraScale -= 0.01f;
            else if (Keyboard.GetState().IsKeyDown(Keys.M) && CameraScale < maxZoomScale) 
                CameraScale += 0.01f;
            foreach (var sys in _systems) sys.Update(gameTime);

            player.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // need an environment drawing system

            //render target to back buffer
            GraphicsDevice.SetRenderTarget(_target);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

            foreach (var sys in _systems) sys.Draw(_spriteBatch);

            _spriteBatch.End();
            GraphicsDevice.SetRenderTarget(null);

            targetBatch.Begin();
            targetBatch.Draw(_target, new Rectangle(
              (int)(GraphicsDevice.DisplayMode.Width * (1 - CameraScale)) / 5,
                (int)(GraphicsDevice.DisplayMode.Height * (1 - CameraScale)) / 5 ,
                (int) (GraphicsDevice.DisplayMode.Width * CameraScale),
                (int) (GraphicsDevice.DisplayMode.Height * CameraScale)), Color.White);
            
            targetBatch.End();


            base.Draw(gameTime);
        }
    }
}