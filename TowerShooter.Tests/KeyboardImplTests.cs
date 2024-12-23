using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework.Input;
using Moq;

namespace TowerShooter.Tests
{
    [TestClass]
    public class KeyboardImplTests
    {
        [TestMethod]
        public void IsSingleKeyPress()
        {
            //Arrange
            Mock<ITowerShooter> towerShooter = new();

            towerShooter.Setup(m => m.KeyboardState.GetState()).Returns(new KeyboardState(new Keys[] { Keys.W }));

            KeyboardImpl keyboard = new(towerShooter.Object);

            //Act
            keyboard.Update();
            bool result = keyboard.IsSingleKeyPress(Keys.W);

            //Assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void IsKeyDown()
        {
            //Arrange
            Mock<ITowerShooter> towerShooter = new();

            towerShooter.Setup(m => m.KeyboardState.GetState()).Returns(new KeyboardState(new Keys[] { Keys.W }));

            KeyboardImpl keyboard = new(towerShooter.Object);

            //Act
            keyboard.Update();
            keyboard.Update();
            bool result = keyboard.IsKeyDown(Keys.W);

            //Assert
            Assert.AreEqual(true, result);
        }
    }
}