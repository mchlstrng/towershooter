using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework.Input;
using Moq;

namespace TowerShooter.Tests
{
    [TestClass]
    public class MouseImplTests
    {
        [TestMethod]
        public void IsSingleLeftClick()
        {
            //Arrange
            Mock<ITowerShooter> towerShooter = new();
            towerShooter.Setup(m => m.MouseState.GetState()).Returns(new MouseState(10, 10, 0, ButtonState.Pressed, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released));
            towerShooter.Setup(m => m.Camera2D.ViewMatrix).Returns(new Microsoft.Xna.Framework.Matrix());
            MouseImpl mouse = new(towerShooter.Object);

            //Act
            mouse.Update();
            bool result = mouse.IsSingleLeftClick();

            //Assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void IsLeftClickFown()
        {
            //Arrange
            Mock<ITowerShooter> towerShooter = new();
            towerShooter.Setup(m => m.MouseState.GetState()).Returns(new MouseState(10, 10, 0, ButtonState.Pressed, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released));
            towerShooter.Setup(m => m.Camera2D.ViewMatrix).Returns(new Microsoft.Xna.Framework.Matrix());
            MouseImpl mouse = new(towerShooter.Object);

            //Act
            mouse.Update();
            mouse.Update();
            bool result = mouse.IsLeftClickDown();

            //Assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void IsSingleRightClick()
        {
            //Arrange
            Mock<ITowerShooter> towerShooter = new();
            towerShooter.Setup(m => m.MouseState.GetState()).Returns(new MouseState(10, 10, 0, ButtonState.Released, ButtonState.Released, ButtonState.Pressed, ButtonState.Released, ButtonState.Released));
            towerShooter.Setup(m => m.Camera2D.ViewMatrix).Returns(new Microsoft.Xna.Framework.Matrix());
            MouseImpl mouse = new(towerShooter.Object);

            //Act
            mouse.Update();
            bool result = mouse.IsSingleRightClick();

            //Assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void IsRightClickDown()
        {
            //Arrange
            Mock<ITowerShooter> towerShooter = new();
            towerShooter.Setup(m => m.MouseState.GetState()).Returns(new MouseState(10, 10, 0, ButtonState.Released, ButtonState.Released, ButtonState.Pressed, ButtonState.Released, ButtonState.Released));
            towerShooter.Setup(m => m.Camera2D.ViewMatrix).Returns(new Microsoft.Xna.Framework.Matrix());
            MouseImpl mouse = new(towerShooter.Object);

            //Act
            mouse.Update();
            mouse.Update();
            bool result = mouse.IsRightClickDown();

            //Assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void GetMouseRectangle()
        {
            //Arrange
            Mock<ITowerShooter> towerShooter = new();
            towerShooter.Setup(m => m.MouseState.GetState()).Returns(new MouseState(368, 408, 0, ButtonState.Released, ButtonState.Released, ButtonState.Pressed, ButtonState.Released, ButtonState.Released));
            towerShooter.Setup(m => m.Camera2D.ViewMatrix).Returns(new Microsoft.Xna.Framework.Matrix
            {
                Backward = new Microsoft.Xna.Framework.Vector3(0, 0, 1),
                Down = new Microsoft.Xna.Framework.Vector3(0, -2.49999f, 0),
                Forward = new Microsoft.Xna.Framework.Vector3(0, 0, -1),
                Left = new Microsoft.Xna.Framework.Vector3(-2.49999f, 0, 0),
                Right = new Microsoft.Xna.Framework.Vector3(2.49999f, 0, 0),
                Translation = new Microsoft.Xna.Framework.Vector3(0, -749.999268f, 0),
                Up = new Microsoft.Xna.Framework.Vector3(0, 2.49999f, 0),
                M11 = 2.24999881f,
                M22 = 2.24999881f,
                M33 = 1,
                M42 = -749.999268f,
                M44 = 1
            });

            MouseImpl mouse = new(towerShooter.Object);

            //Act
            mouse.Update();
            Microsoft.Xna.Framework.Rectangle result = mouse.GetMouseRectangle();

            //Assert
            Assert.AreEqual(163, result.X);
            Assert.AreEqual(514, result.Y);
        }

        [TestMethod]
        public void IsScrollUpTest()
        {
            //Arrange
            Mock<ITowerShooter> towerShooter = new();
            towerShooter.Setup(m => m.MouseState.GetState()).Returns(new MouseState(368, 408, 0, ButtonState.Released, ButtonState.Released, ButtonState.Pressed, ButtonState.Released, ButtonState.Released));
            towerShooter.Setup(m => m.Camera2D.ViewMatrix).Returns(new Microsoft.Xna.Framework.Matrix
            {
                Backward = new Microsoft.Xna.Framework.Vector3(0, 0, 1),
                Down = new Microsoft.Xna.Framework.Vector3(0, -2.49999f, 0),
                Forward = new Microsoft.Xna.Framework.Vector3(0, 0, -1),
                Left = new Microsoft.Xna.Framework.Vector3(-2.49999f, 0, 0),
                Right = new Microsoft.Xna.Framework.Vector3(2.49999f, 0, 0),
                Translation = new Microsoft.Xna.Framework.Vector3(0, -749.999268f, 0),
                Up = new Microsoft.Xna.Framework.Vector3(0, 2.49999f, 0),
                M11 = 2.24999881f,
                M22 = 2.24999881f,
                M33 = 1,
                M42 = -749.999268f,
                M44 = 1
            });

            MouseImpl mouse = new(towerShooter.Object);

            //Act
            mouse.Update();
            bool result = mouse.IsScrollUp();

            //Assert
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void IsScrollDownTest()
        {
            //Arrange
            Mock<ITowerShooter> towerShooter = new();
            towerShooter.Setup(m => m.MouseState.GetState()).Returns(new MouseState(368, 408, 0, ButtonState.Released, ButtonState.Released, ButtonState.Pressed, ButtonState.Released, ButtonState.Released));
            towerShooter.Setup(m => m.Camera2D.ViewMatrix).Returns(new Microsoft.Xna.Framework.Matrix
            {
                Backward = new Microsoft.Xna.Framework.Vector3(0, 0, 1),
                Down = new Microsoft.Xna.Framework.Vector3(0, -2.49999f, 0),
                Forward = new Microsoft.Xna.Framework.Vector3(0, 0, -1),
                Left = new Microsoft.Xna.Framework.Vector3(-2.49999f, 0, 0),
                Right = new Microsoft.Xna.Framework.Vector3(2.49999f, 0, 0),
                Translation = new Microsoft.Xna.Framework.Vector3(0, -749.999268f, 0),
                Up = new Microsoft.Xna.Framework.Vector3(0, 2.49999f, 0),
                M11 = 2.24999881f,
                M22 = 2.24999881f,
                M33 = 1,
                M42 = -749.999268f,
                M44 = 1
            });

            MouseImpl mouse = new(towerShooter.Object);

            //Act
            mouse.Update();
            bool result = mouse.IsScrollDown();

            //Assert
            Assert.AreEqual(false, result);
        }
    }
}
