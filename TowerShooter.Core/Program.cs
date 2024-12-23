using Microsoft.Xna.Framework;
using System;

namespace TowerShooter
{
    /// <summary>
    /// Composition root for the game. This is where all the dependencies are wired up.
    /// </summary>
    public static class Program
    {
        [STAThread] // STAThread means that the game will run on a single thread.
        private static void Main()
        {
            using TowerShooter towerShooter = new()
            {
                Content = { RootDirectory = "Content" },
                ResolutionX = 800,
                ResolutionY = 600
            };

            ScreenFactory screenFactory = new(towerShooter);

            towerShooter.ScreenFactory = screenFactory;

            towerShooter.ScreenManager = new ScreenManager(screenFactory);

            towerShooter.Camera2D = new Camera2D(towerShooter);

            towerShooter.Keyboard = new KeyboardImpl(towerShooter);

            towerShooter.KeyboardState = new KeyboardStateImpl();

            towerShooter.Mouse = new MouseImpl(towerShooter);

            towerShooter.MouseState = new MouseStateImpl();

            towerShooter.InputManager = new InputManager(towerShooter);

            towerShooter.GraphicsDeviceManager = new GraphicsDeviceManagerImpl(towerShooter);

            towerShooter.FramesPerSecondCounter = new FpsCounter();

            towerShooter.SpriteBatch = new SpriteBatchImpl(towerShooter);

            towerShooter.ContentManager = new ContentManagerImpl(towerShooter.Content);

            towerShooter.SettingsManager = new SettingsManager(towerShooter);

            towerShooter.SettingsManager.SetIsCreative();

            towerShooter.RainEngine = new RainEngine(towerShooter);

            towerShooter.GameClock = new GameClock(towerShooter);

            towerShooter.BlockTypeToTextureService = new BlockTypeToTextureService(towerShooter);

            towerShooter.Inventory = new Inventory(towerShooter);

            towerShooter.Player = new Player(towerShooter);

            towerShooter.WeatherData = new WeatherData(towerShooter);

            towerShooter.FlashlightComponent = new PenumbraWrapper(towerShooter);

            towerShooter.GamepadState = new GamepadStateImpl();

            towerShooter.Gamepad = new GamepadImpl(towerShooter);

            towerShooter.DebugService = new DebugService(towerShooter);

            towerShooter.SoundEffectPlayer = new SoundEffectPlayer();

            towerShooter.GameTime = new GameTime();

            //start the game
            towerShooter.Run();
        }
    }
}