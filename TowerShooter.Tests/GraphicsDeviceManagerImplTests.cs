using Microsoft.Xna.Framework;

namespace TowerShooter.Tests
{
    [TestClass]
    public class GraphicsDeviceManagerImplTests
    {
        [TestMethod]
        public void ApplyChangesTest()
        {
            //Arrange
            Game game = new();
            GraphicsDeviceManagerImpl graphicsDeviceManagerImpl = new(game)
            {
                PreferredBackBufferWidth = 800,
                PreferredBackBufferHeight = 600
            };

            //Act
            graphicsDeviceManagerImpl.ApplyChanges();

            //Assert
            Assert.AreEqual(800, graphicsDeviceManagerImpl.GraphicsDevice.Viewport.Width);
            Assert.AreEqual(600, graphicsDeviceManagerImpl.GraphicsDevice.Viewport.Height);
        }
    }
}