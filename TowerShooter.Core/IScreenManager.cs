using Microsoft.Xna.Framework;
using System;
using TowerShooter.Screens.Interfaces;

namespace TowerShooter
{
    public interface IScreenManager
    {
        void Draw(ISpriteBatch spriteBatch, GameTime gameTime);
        void Update(GameTime gameTime);
        void SetActiveScreen(IGameScreen newGameScreen);
        void GetNewScreenInstance(ScreenType type);
    }
}