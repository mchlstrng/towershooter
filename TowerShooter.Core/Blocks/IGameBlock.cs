using System;
using Microsoft.Xna.Framework;

namespace TowerShooter.Blocks
{
    public interface IGameBlock
    {
        void Draw(ISpriteBatch spriteBatch, GameTime gameTime);
        void Update(GameTime gameTime);
        bool IsVisible { get; set; }
        bool IsSolid { get; set; }
        Guid Guid { get; set; }

        Rectangle GetRectangle();
        Vector2 GetPosition();
        Vector2 GetSize();

        bool LosCollision { get; set; }
        BlockType BlockType { get; set; }
        int Health { get; set; }
        void Mine();
        bool ItemCanSpawnOnTop { get; set; }
    }
}