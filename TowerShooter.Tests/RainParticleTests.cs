using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Moq;

namespace TowerShooter.Tests
{
    [TestClass()]
    public class RainParticleTests
    {
        [TestMethod()]
        public void Draw()
        {
            //Arrange
            Mock<ITowerShooter> towerShooter = new();
            Mock<ITexture2D> texture = new();
            texture.Setup(m => m.Width).Returns(2);
            texture.Setup(m => m.Height).Returns(5);
            Mock<IContentManager> contentManager = new();
            contentManager.Setup(m => m.LoadTexture2D("rain")).Returns(texture.Object);
            towerShooter.Setup(m => m.ContentManager).Returns(contentManager.Object);
            Mock<ISpriteBatch> spriteBatch = new();
            towerShooter.Setup(m => m.SpriteBatch).Returns(spriteBatch.Object);
            Vector2 position = new(20, 40);
            RainParticle rainParticle = new(towerShooter.Object, position);
            rainParticle.Velocity = new Vector2(2, 20);

            //Act
            rainParticle.Draw(towerShooter.Object.SpriteBatch, new GameTime());

            //Assert
            spriteBatch.Verify(m => m.Draw(It.IsAny<Texture2D>(), position, null, Color.White, 1.47112763f, new Vector2(1, 2), 1, SpriteEffects.None, 1));
        }
    }
}