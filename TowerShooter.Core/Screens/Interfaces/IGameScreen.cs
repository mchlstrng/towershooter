using Microsoft.Xna.Framework;

namespace TowerShooter.Screens.Interfaces
{
    public interface IGameScreen
    {
        void Update(GameTime gameTime);
        void Draw(ISpriteBatch spriteBatch, GameTime gameTime);
        void LoadContent();
    }
}