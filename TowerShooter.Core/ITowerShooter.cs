using Microsoft.Xna.Framework;
using TowerShooter.Screens;

namespace TowerShooter
{
    public interface ITowerShooter
    {
        ICamera2D Camera2D { get; set; }
        bool Debug { get; set; }
        IFpsCounter FramesPerSecondCounter { get; set; }
        int ResolutionY { get; set; }
        int ResolutionX { get; set; }
        IGraphicsDeviceManagerImpl GraphicsDeviceManager { get; set; }
        IInputManager InputManager { get; set; }
        IScreenManager ScreenManager { get; set; }
        ISpriteBatch SpriteBatch { get; set; }
        IContentManager ContentManager { get; set; }
        ISettingsManager SettingsManager { get; set; }
        IMouse Mouse { get; set; }
        IKeyboard Keyboard { get; set; }
        IMouseStateImpl MouseState { get; set; }
        IKeyboardStateImpl KeyboardState { get; set; }
        IRainEngine RainEngine { get; set; }
        IGameClock GameClock { get; set; }
        IGameplayGameScreen GameplayGameScreen { get; set; }
        IBlockTypeToTextureService BlockTypeToTextureService { get; set; }
        IInventory Inventory { get; set; }
        IPlayer Player { get; set; }
        IWeatherData WeatherData { get; set; }
        int MapWidth { get; set; }
        int MapHeight { get; set; }
        IFlashlightComponent FlashlightComponent { get; set; }
        void Exit();
        IScreenFactory ScreenFactory { get; set; }
        bool IsCreative { get; set; }
        public IGamepadState GamepadState { get; set; }
        public IGamepad Gamepad { get; set; }
        ISoundEffectPlayer SoundEffectPlayer { get; set; }
        GameTime GameTime { get; set; }
    }
}