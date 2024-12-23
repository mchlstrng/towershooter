using Microsoft.Xna.Framework;
using Moq;
using TowerShooter.Screens;
using TowerShooter.Screens.Interfaces;

namespace TowerShooter.Tests
{
    [TestClass]
    public class ScreenManagerTests
    {
        [TestMethod]
        public void Update()
        {
            //Arrange
            Mock<IScreenFactory> screenFactory = new();
            ScreenManager screenManager = new(screenFactory.Object);
            Mock<IGameScreen> gameScreen = new();
            GameTime gameTime = new();

            //Act
            screenManager.SetActiveScreen(gameScreen.Object);
            screenManager.Update(gameTime);

            //Assert
            gameScreen.Verify(m => m.Update(gameTime));
        }

        [TestMethod]
        public void Draw()
        {
            //Arrange
            Mock<IScreenFactory> screenFactory = new();
            GameTime gameTime = new();
            Mock<ISpriteBatch> spriteBatch = new();
            Mock<IGameScreen> gameScreen = new();
            ScreenManager screenManager = new(screenFactory.Object);

            //Act
            screenManager.SetActiveScreen(gameScreen.Object);
            screenManager.Draw(spriteBatch.Object, gameTime);

            //Assert
            gameScreen.Verify(m => m.Draw(spriteBatch.Object, gameTime));
        }

        [TestMethod]
        public void SetActiveScreen()
        {
            //Arrange
            Mock<IScreenFactory> screenFactory = new();
            ScreenManager screenManager = new(screenFactory.Object);
            Mock<IGameScreen> gameScreen = new();

            //Act
            screenManager.SetActiveScreen(gameScreen.Object);

            //Assert
            gameScreen.Verify(m => m.LoadContent());
        }

        [TestMethod]
        public void GetNewScreenInstance()
        {
            //Arrange
            Mock<IGameScreen> gameScreen = new();
            Mock<IScreenFactory> screenFactory = new();
            screenFactory.Setup(m => m.GetNewScreenInstance(ScreenType.MainMenu)).Returns(gameScreen.Object);

            ScreenManager screenManager = new(screenFactory.Object);

            //Act
            screenManager.GetNewScreenInstance(ScreenType.MainMenu);

            //Assert
            gameScreen.Verify(m => m.LoadContent());
        }
    }
}