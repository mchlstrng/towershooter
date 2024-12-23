using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerShooter.Screens.Interfaces;

namespace TowerShooter.Screens
{
    public class GameoverMenuGameScreen : IGameScreen, IGameoverMenuGameScreen
    {
        private readonly ITowerShooter _towerShooter;
        private SpriteFont _font;
        private ISoundEffect soundEffect;

        public GameoverMenuGameScreen(ITowerShooter towerShooter)
        {
            _towerShooter = towerShooter;
        }

        public void Draw(ISpriteBatch spriteBatch, GameTime gameTime)
        {
            const string text = "You died. Press ENTER to return to main menu.";

            float xPos = (_towerShooter.GraphicsDeviceManager.PreferredBackBufferWidth / 2f) - (_font.MeasureString(text).X / 2);
            float yPos = (_towerShooter.GraphicsDeviceManager.PreferredBackBufferHeight / 2f) - (_font.MeasureString(text).Y / 2);
            spriteBatch.Begin();
            spriteBatch.DrawString(_font, text, new Vector2(xPos, yPos), Color.White);
            spriteBatch.End();
        }

        public void LoadContent()
        {
            _font = _towerShooter.ContentManager.Load<SpriteFont>("menuItemFont");
            soundEffect = _towerShooter.ContentManager.LoadSoundEffect("gameover");
            soundEffect.Play();
        }

        public void Update(GameTime gameTime)
        {
            if (_towerShooter.InputManager.IsSingleKeyPress(Microsoft.Xna.Framework.Input.Keys.Enter))
            {
                _towerShooter.ScreenManager.GetNewScreenInstance(ScreenType.MainMenu);
            }
        }
    }
}