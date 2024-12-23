using Microsoft.Xna.Framework;

namespace TowerShooter
{
    public interface IGameoverMenuGameScreen
    {
        void Draw(ISpriteBatch spriteBatch, GameTime gameTime);
        void LoadContent();
        void Update(GameTime gameTime);
    }
}