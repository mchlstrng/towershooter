using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Moq;

namespace TowerShooter.Tests
{
    public class InputManagerWithMocks
    {
        public Mock<ITowerShooter> TowerShooter { get; set; } = null!;
        public InputManager InputManager { get; set; } = null!;
    }

    [TestClass]
    public class InputManagerTests
    {
        public static InputManagerWithMocks GetInputManagerWithMocks()
        {
            Mock<ITowerShooter> towerShooter = new();
            towerShooter.Setup(m => m.Mouse).Returns(new Mock<IMouse>().Object);
            towerShooter.Setup(m => m.Keyboard).Returns(new Mock<IKeyboard>().Object);
            towerShooter.Setup(m => m.Gamepad).Returns(new Mock<IGamepad>().Object);

            InputManager inputManager = new(towerShooter.Object);

            return new InputManagerWithMocks
            {
                TowerShooter = towerShooter,
                InputManager = inputManager
            };
        }

        [TestMethod]
        public void IsSingleKeyPress()
        {
            //Arrange
            Mock<IKeyboard> keyboard = new();
            keyboard.Setup(m => m.IsSingleKeyPress(Keys.W)).Returns(true);
            InputManagerWithMocks inputManagerWithMocks = GetInputManagerWithMocks();
            inputManagerWithMocks.TowerShooter.Setup(m => m.Keyboard).Returns(keyboard.Object);

            InputManager inputManager = inputManagerWithMocks.InputManager;

            //Act
            inputManager.Update(new GameTime());
            bool result = inputManager.IsSingleKeyPress(Keys.W);

            //Assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void IsKeyDown()
        {
            //Arrange
            Mock<IKeyboard> keyboard = new();
            keyboard.Setup(m => m.IsKeyDown(Keys.W)).Returns(true);
            InputManagerWithMocks inputManagerWithMocks = GetInputManagerWithMocks();
            inputManagerWithMocks.TowerShooter.Setup(m => m.Keyboard).Returns(keyboard.Object);

            InputManager inputManager = inputManagerWithMocks.InputManager;

            //Act
            inputManager.Update(new GameTime());
            bool result = inputManager.IsKeyDown(Keys.W);

            //Assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void IsSingleLeftClick()
        {
            //Arrange
            Mock<IMouse> mouse = new();
            mouse.Setup(m => m.IsSingleLeftClick()).Returns(true);
            InputManagerWithMocks inputManagerWithMocks = GetInputManagerWithMocks();
            inputManagerWithMocks.TowerShooter.Setup(m => m.Mouse).Returns(mouse.Object);

            //Act
            bool result = inputManagerWithMocks.InputManager.IsSingleLeftClick();

            //Assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void IsLeftClickDown()
        {
            //Arrange
            Mock<IMouse> mouse = new();
            mouse.Setup(m => m.IsLeftClickDown()).Returns(true);
            InputManagerWithMocks inputManagerWithMocks = GetInputManagerWithMocks();
            inputManagerWithMocks.TowerShooter.Setup(m => m.Mouse).Returns(mouse.Object);

            //Act
            bool result = inputManagerWithMocks.InputManager.IsLeftClickDown();

            //Assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void IsSingleRightClick()
        {
            //Arrange
            Mock<IMouse> mouse = new();
            mouse.Setup(m => m.IsSingleRightClick()).Returns(true);
            InputManagerWithMocks inputManagerWithMocks = GetInputManagerWithMocks();
            inputManagerWithMocks.TowerShooter.Setup(m => m.Mouse).Returns(mouse.Object);

            //Act
            bool result = inputManagerWithMocks.InputManager.IsSingleRightClick();

            //Assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void IsRightClickDown()
        {
            //Arrange
            Mock<IMouse> mouse = new();
            mouse.Setup(m => m.IsRightClickDown()).Returns(true);
            InputManagerWithMocks inputManagerWithMocks = GetInputManagerWithMocks();
            inputManagerWithMocks.TowerShooter.Setup(m => m.Mouse).Returns(mouse.Object);

            //Act
            bool result = inputManagerWithMocks.InputManager.IsRightClickDown();

            //Assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void IsScrollUp()
        {
            //Arrange
            Mock<IMouse> mouse = new();
            mouse.Setup(m => m.IsScrollUp()).Returns(true);
            InputManagerWithMocks inputManagerWithMocks = GetInputManagerWithMocks();
            inputManagerWithMocks.TowerShooter.Setup(m => m.Mouse).Returns(mouse.Object);

            //Act
            bool result = inputManagerWithMocks.InputManager.IsScrollUp();

            //Assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void IsScrollDown()
        {
            //Arrange
            Mock<IMouse> mouse = new();
            mouse.Setup(m => m.IsScrollDown()).Returns(true);
            InputManagerWithMocks inputManagerWithMocks = GetInputManagerWithMocks();
            inputManagerWithMocks.TowerShooter.Setup(m => m.Mouse).Returns(mouse.Object);

            //Act
            bool result = inputManagerWithMocks.InputManager.IsScrollDown();

            //Assert
            Assert.AreEqual(true, result);
        }
    }
}