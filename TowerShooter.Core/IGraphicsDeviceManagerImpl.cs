using Microsoft.Xna.Framework.Graphics;

namespace TowerShooter
{
    public interface IGraphicsDeviceManagerImpl
    {
        int PreferredBackBufferWidth { get; set; }
        int PreferredBackBufferHeight { get; set; }
        void ApplyChanges();
        GraphicsDevice GraphicsDevice { get; set; }
    }
}