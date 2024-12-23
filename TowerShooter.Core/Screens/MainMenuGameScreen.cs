using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using TowerShooter.Screens.Interfaces;

namespace TowerShooter.Screens
{
    public class MainMenuGameScreen : IGameScreen, IMainMenuGameScreen
    {
        private ISpriteFont _menuItemFont;
        private ISpriteFont _titleFont;
        private readonly ITowerShooter _towerShooter;
        private ISoundEffect selectSound;
        private ISoundEffect changeSelectSound;

        private readonly List<string> _menuItems = new()
        {
            "Start",
            "Options",
            "Exit"
        };

        private int _selectedMenuItem = 0;

        public MainMenuGameScreen(ITowerShooter towerShooter)
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

                spriteBatch.DrawString(_menuItemFont.GetSpriteFont(), _menuItems[i], Util.GetRoundedVector2(new Vector2(xPos, yPos)), menuItemColor);

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
                if (_menuItems[_selectedMenuItem] == "Start")
                {
                    selectSound.Play();
                    _towerShooter.ScreenManager.GetNewScreenInstance(ScreenType.Gameplay);
                    _towerShooter.GameClock.SetStartHour(8);
                }

                if (_menuItems[_selectedMenuItem] == "Options")
                {
                    selectSound.Play();
                    _towerShooter.ScreenManager.GetNewScreenInstance(ScreenType.OptionsMenu);
                }

                if (_menuItems[_selectedMenuItem] == "Exit")
                {
                    selectSound.Play();
                    _towerShooter.Exit();
                }
            }
        }

        public void LoadContent()
        {
            _towerShooter.FlashlightComponent.Disable();
            _menuItemFont = _towerShooter.ContentManager.LoadSpriteFont("MenuItemFont");
            _titleFont = _towerShooter.ContentManager.LoadSpriteFont("TitleFont");
            selectSound = _towerShooter.ContentManager.LoadSoundEffect("select");
            changeSelectSound = _towerShooter.ContentManager.LoadSoundEffect("changeselect");
        }
    }
}