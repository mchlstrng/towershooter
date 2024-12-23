using Microsoft.Xna.Framework;
using System;
using TowerShooter.Screens.Interfaces;

namespace TowerShooter
{
    public class ScreenManager : IScreenManager
    {
        private IGameScreen _activeGameScreen;
        private readonly IScreenFactory screenFactory;

        public ScreenManager(IScreenFactory screenFactory)
        {
            this.screenFactory = screenFactory ?? throw new ArgumentNullException(nameof(screenFactory));
        }

        public void Update(GameTime gameTime)
        {
            _activeGameScreen.Update(gameTime);
        }

        public void Draw(ISpriteBatch spriteBatch, GameTime gameTime)
        {
            _activeGameScreen.Draw(spriteBatch, gameTime);
        }

        public void SetActiveScreen(IGameScreen newGameScreen)
        {
            _activeGameScreen = newGameScreen;
            _activeGameScreen.LoadContent();
        }

        public void GetNewScreenInstance(ScreenType type)
        {
            IGameScreen screen = screenFactory.GetNewScreenInstance(type);
            SetActiveScreen(screen);
        }
    }
}