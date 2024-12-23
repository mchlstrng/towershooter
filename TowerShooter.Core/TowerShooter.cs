using Microsoft.Xna.Framework;
using TowerShooter.Screens;

namespace TowerShooter
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class TowerShooter : Game, ITowerShooter
    {
        public int ResolutionX { get; set; }
        public int ResolutionY { get; set; }
        public int MapWidth { get; set; }
        public int MapHeight { get; set; }
        public IGraphicsDeviceManagerImpl GraphicsDeviceManager { get; set; }
        public IScreenManager ScreenManager { get; set; }
        public bool Debug { get; set; }
        public IInputManager InputManager { get; set; }
        public ISpriteBatch SpriteBatch { get; set; }
        public IFpsCounter FramesPerSecondCounter { get; set; }
        public ICamera2D Camera2D { get; set; }
        public IContentManager ContentManager { get; set; }
        public ISettingsManager SettingsManager { get; set; }
        public IKeyboard Keyboard { get; set; }
        public IKeyboardStateImpl KeyboardState { get; set; }
        public IMouseStateImpl MouseState { get; set; }
        public IMouse Mouse { get; set; }
        public IRainEngine RainEngine { get; set; }
        public IGameClock GameClock { get; set; }
        public IGameplayGameScreen GameplayGameScreen { get; set; }
        public IBlockTypeToTextureService BlockTypeToTextureService { get; set; }
        public IInventory Inventory { get; set; }
        public IPlayer Player { get; set; }
        public IWeatherData WeatherData { get; set; }
        public IFlashlightComponent FlashlightComponent { get; set; }
        public IScreenFactory ScreenFactory { get; set; }
        public bool IsCreative { get; set; }
        public IGamepadState GamepadState { get; set; }
        public IGamepad Gamepad { get; set; }
        public IDebugService DebugService { get; set; }
        public ISoundEffectPlayer SoundEffectPlayer { get; set; }
        public GameTime GameTime { get; set; }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            GraphicsDeviceManager.PreferredBackBufferWidth = ResolutionX;
            GraphicsDeviceManager.PreferredBackBufferHeight = ResolutionY;
            GraphicsDeviceManager.ApplyChanges();

            IsMouseVisible = true;
            FlashlightComponent.Initialize();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            SpriteBatch.LoadContent();
            ScreenManager.GetNewScreenInstance(ScreenType.MainMenu);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            GameTime = gameTime;

            Window.Title = $"TowerShooter | FPS: {FramesPerSecondCounter.FramesPerSecond}";

            ScreenManager.Update(gameTime);
            FramesPerSecondCounter.Update(gameTime);
            InputManager.Update(gameTime);
            Camera2D.Update();
            DebugService.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            FlashlightComponent.BeginDraw();
            GraphicsDevice.Clear(Color.Black);

            FramesPerSecondCounter.Draw(gameTime);
            ScreenManager.Draw(SpriteBatch, gameTime);

            base.Draw(gameTime);
        }
    }
}