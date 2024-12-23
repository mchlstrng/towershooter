using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using TowerShooter.Screens.Interfaces;

namespace TowerShooter.Screens
{

    public class OptionsMenuGameScreen : IGameScreen, IOptionsMenuGameScreen
    {
        private ISpriteFont _menuItemFont;
        private ISpriteFont _titleFont;
        private readonly ITowerShooter _towerShooter;
        private ISoundEffect selectSound;
        private ISoundEffect changeSelectSound;

        private readonly List<string> _menuItems = new();

        private int _selectedMenuItem = 0;

        private Settings settings;

        public OptionsMenuGameScreen(ITowerShooter towerShooter)
        {
            _towerShooter = towerShooter;
        }

        public void Draw(ISpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            DrawGameTitle(spriteBatch);
            DrawMenuItems(spriteBatch);
            spriteBatch.End();
        }

        private void DrawMenuItems(ISpriteBatch spriteBatch)
        {
            _menuItems.Clear();
            foreach (SettingItem setting in settings.SettingItems)
            {
                _menuItems.Add(setting.Name + ": " + setting.CurrentValue);
            }
            _menuItems.Add("Back to main menu");


            float totalMenuItemsY = 0f;
            foreach (string menuItem in _menuItems)
            {
                totalMenuItemsY += _menuItemFont.MeasureString(menuItem).Y;
            }

            float yPos = (_towerShooter.GraphicsDeviceManager.PreferredBackBufferHeight / 2f) - (totalMenuItemsY / 2f);

            for (int i = 0; i < _menuItems.Count; i++)
            {
                Color menuItemColor = Color.White;

                if (_selectedMenuItem == i)
                {
                    menuItemColor = Color.Yellow;
                }

                float xPos = (_towerShooter.GraphicsDeviceManager.PreferredBackBufferWidth / 2f) - (_menuItemFont.MeasureString(_menuItems[i]).X / 2f);

                spriteBatch.DrawString(_menuItemFont.GetSpriteFont(), _menuItems[i], new Vector2(xPos, yPos), menuItemColor);

                yPos += _menuItemFont.MeasureString(_menuItems[i]).Y + 10f;
            }
        }

        private void DrawGameTitle(ISpriteBatch spriteBatch)
        {
            const string TitleText = "TowerShooter";
            float xPos = (_towerShooter.GraphicsDeviceManager.PreferredBackBufferWidth / 2) - (_titleFont.MeasureString(TitleText).X / 2);
            const float yPos = 10f;
            spriteBatch.DrawString(_titleFont.GetSpriteFont(), TitleText, new Vector2(xPos, yPos), Color.White);
        }

        public void Update(GameTime gameTime)
        {
            _towerShooter.Camera2D.ResetToNormal();
            if (_towerShooter.InputManager.IsSingleKeyPress(Keys.Up))
            {
                _selectedMenuItem--;
                changeSelectSound.Play();
            }
            if (_towerShooter.InputManager.IsSingleKeyPress(Keys.Down))
            {
                _selectedMenuItem++;
                changeSelectSound.Play();
            }
            _selectedMenuItem = MathHelper.Clamp(_selectedMenuItem, 0, _menuItems.Count - 1);

            if (_towerShooter.InputManager.IsSingleKeyPress(Keys.Enter))
            {
                selectSound.Play();
                if (_menuItems[_selectedMenuItem].StartsWith(SettingNames.CreativeMode))
                {
                    SettingItem setting = settings.SettingItems.Single(s => s.Name == SettingNames.CreativeMode);
                    setting.SetNextSettingValue();
                }

                if (_menuItems[_selectedMenuItem] == "Back to main menu")
                {
                    //save settings and load main menu screen

                    _towerShooter.SettingsManager.SaveSettings(settings);
                    _towerShooter.SettingsManager.SetIsCreative();

                    _towerShooter.ScreenManager.GetNewScreenInstance(ScreenType.MainMenu);
                }
            }
        }

        public void LoadContent()
        {
            _menuItemFont = _towerShooter.ContentManager.LoadSpriteFont("MenuItemFont");
            _titleFont = _towerShooter.ContentManager.LoadSpriteFont("TitleFont");
            selectSound = _towerShooter.ContentManager.LoadSoundEffect("select");
            changeSelectSound = _towerShooter.ContentManager.LoadSoundEffect("changeselect");
            settings = _towerShooter.SettingsManager.GetSettings();
        }
    }
}