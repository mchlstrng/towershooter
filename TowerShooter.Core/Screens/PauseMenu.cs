using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TowerShooter.Screens.Interfaces;

namespace TowerShooter.Screens
{
    internal class PauseMenu : IGameScreen
    {
        private SpriteFont _menuItemFont;
        private readonly GameplayGameScreen _gameplayGameScreen;

        public PauseMenu(GameplayGameScreen gameplayGameScreen)
        {
            _gameplayGameScreen = gameplayGameScreen;
        }

        public void Draw(ISpriteBatch spriteBatch, GameTime gameTime)
        {
            _gameplayGameScreen._towerShooter.Camera2D.ResetToNormal();
            DrawText(spriteBatch);
        }

        private void DrawText(ISpriteBatch spriteBatch)
        {
            const string TitleText = "Game paused. ESC=QUIT | ENTER=CONTINUE";
            float xPos = (_gameplayGameScreen._towerShooter.GraphicsDeviceManager.PreferredBackBufferWidth / 2) - (_menuItemFont.MeasureString(TitleText).X / 2);
            float yPos = (_gameplayGameScreen._towerShooter.GraphicsDeviceManager.PreferredBackBufferHeight / 2) - (_menuItemFont.MeasureString(TitleText).Y / 2);
            spriteBatch.Begin();
            spriteBatch.DrawString(_menuItemFont, TitleText, new Vector2(xPos, yPos), Color.White);
            spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            if (_gameplayGameScreen._towerShooter.InputManager.IsSingleKeyPress(Keys.Escape))
            {
                _gameplayGameScreen._towerShooter.ScreenManager.GetNewScreenInstance(ScreenType.MainMenu);
            }
            if (_gameplayGameScreen._towerShooter.InputManager.IsSingleKeyPress(Keys.Enter))
            {
                _gameplayGameScreen.GameRunning = true;
            }
        }

        public void LoadContent()
        {
            _menuItemFont = _gameplayGameScreen._towerShooter.ContentManager.Load<SpriteFont>("MenuItemFont");
        }
    }
}