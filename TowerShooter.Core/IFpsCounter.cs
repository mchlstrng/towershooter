using Microsoft.Xna.Framework;

namespace TowerShooter
{
    public interface IFpsCounter
    {
        int FramesPerSecond { get; }
        void Draw(GameTime gameTime);
        void Update(GameTime gameTime);
    }
}