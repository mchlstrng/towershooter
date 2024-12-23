using Microsoft.Xna.Framework;
using TowerShooter.Blocks;

namespace TowerShooter
{
    public interface IPlayer
    {
        bool CanPlace { get; set; }
        bool CanRemove { get; set; }
        int Hp { get; set; }
        bool DoesNotCollideWithSolidBlock();
        void Draw(ISpriteBatch spriteBatch, GameTime gameTime);
        void UpdateMouseoverBlock(IGameBlock block);
        void Update(GameTime gameTime);
        Vector2 GetPosition();
        void LoadContent();
    }
}