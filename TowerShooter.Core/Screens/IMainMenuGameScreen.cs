using Microsoft.Xna.Framework;

namespace TowerShooter.Screens
{
    public interface IMainMenuGameScreen
    {
        void Draw(ISpriteBatch spriteBatch, GameTime gameTime);
        void LoadContent();
        void Update(GameTime gameTime);
    }
}