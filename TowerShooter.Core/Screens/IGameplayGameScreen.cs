using Microsoft.Xna.Framework;
using System.Collections.Generic;
using TowerShooter.Blocks;

namespace TowerShooter.Screens
{
    public interface IGameplayGameScreen
    {
        List<IGameBlock> Blocks { get; set; }
        void Draw(ISpriteBatch spriteBatch, GameTime gameTime);
        void LoadContent();
        void Update(GameTime gameTime);
    }
}