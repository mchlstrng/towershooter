using Microsoft.Xna.Framework;

namespace TowerShooter.Screens
{
    public interface IOptionsMenuGameScreen
    {
        void Draw(ISpriteBatch spriteBatch, GameTime gameTime);
        void LoadContent();
        void Update(GameTime gameTime);
    }
}