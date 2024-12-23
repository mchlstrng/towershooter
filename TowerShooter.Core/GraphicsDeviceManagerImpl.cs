using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerShooter
{
    public class GraphicsDeviceManagerImpl : IGraphicsDeviceManagerImpl
    {
        private readonly GraphicsDeviceManager _graphicsDeviceManager;

        public GraphicsDeviceManagerImpl(Game game)
        {
            _graphicsDeviceManager = new GraphicsDeviceManager(game);
        }

        public int PreferredBackBufferWidth { get => _graphicsDeviceManager.PreferredBackBufferWidth; set => _graphicsDeviceManager.PreferredBackBufferWidth = value; }
        public int PreferredBackBufferHeight { get => _graphicsDeviceManager.PreferredBackBufferHeight; set => _graphicsDeviceManager.PreferredBackBufferHeight = value; }
        public GraphicsDevice GraphicsDevice { get => _graphicsDeviceManager.GraphicsDevice; set => throw new System.NotImplementedException(); }

        public void ApplyChanges()
        {
            _graphicsDeviceManager.ApplyChanges();
        }
    }
}