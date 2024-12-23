using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Moq;

namespace TowerShooter.Tests
{
    [TestClass]
    public class Camera2DTests
    {
        [TestMethod]
        public void InitTestDefaultValues()
        {
            //Arrange
            Mock<ITowerShooter> towerShooter = new();
            towerShooter.Setup(m => m.ResolutionX).Returns(800);
            towerShooter.Setup(m => m.ResolutionY).Returns(600);

            //Act
            Camera2D cam = new(towerShooter.Object);

            //Assert
            Assert.AreEqual(new Vector2(400, 300), cam.Position);
            Assert.AreEqual(1, cam.Zoom);
        }

        [TestMethod]
        public void MoveTest()
        {
            //Arrange
            Mock<ITowerShooter> ts = new();
            Camera2D cam = new(ts.Object);

            //Act
            cam.Move(new Vector2(1, 1));

            //Assert
            Assert.AreEqual(new Vector2(1, 1), cam.Position);
        }

        [TestMethod]
        public void UpdateZoomInNoClamp()
        {
            //Arrange
            Mock<ITowerShooter> towershooter = new();
            towershooter.Setup(m => m.InputManager.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Q)).Returns(true);
            towershooter.Setup(m => m.Debug).Returns(true);
            Camera2D camera = new(towershooter.Object);

            //Act
            camera.Update();

            //Assert
            Assert.AreEqual(1.05f, camera.Zoom);
        }

        [TestMethod]
        public void UpdateZoomInClamp()
        {
            //Arrange
            Mock<ITowerShooter> towershooter = new();
            towershooter.Setup(m => m.InputManager.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Q)).Returns(true);
            Camera2D camera = new(towershooter.Object)
            {
                Zoom = 4f
            };
            //Act
            camera.Update();

            //Assert
            Assert.AreEqual(4f, camera.Zoom);
        }

        [TestMethod]
        public void UpdateZoomOutNoClamp()
        {
            //Arrange
            Mock<ITowerShooter> towershooter = new();
            towershooter.Setup(m => m.InputManager.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Z)).Returns(true);
            towershooter.Setup(m => m.Debug).Returns(true);
            Camera2D camera = new(towershooter.Object)
            {
                Zoom = 2
            };

            //Act
            camera.Update();

            //Assert
            Assert.AreEqual(1.95f, camera.Zoom);
        }

        [TestMethod]
        public void UpdateZoomOutClamp()
        {
            //Arrange
            Mock<ITowerShooter> towershooter = new();
            towershooter.Setup(m => m.InputManager.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Z)).Returns(true);
            Camera2D camera = new(towershooter.Object)
            {
                Zoom = 1f
            };
            //Act
            camera.Update();

            //Assert
            Assert.AreEqual(1f, camera.Zoom);
        }

        [TestMethod]
        public void ResetToNormal()
        {
            //Arrange
            Mock<ITowerShooter> towershooter = new();
            towershooter.Setup(m => m.ResolutionX).Returns(800);
            towershooter.Setup(m => m.ResolutionY).Returns(600);
            Camera2D camera = new(towershooter.Object)
            {
                Zoom = 3
            };

            //Act
            camera.ResetToNormal();

            //Assert
            Assert.AreEqual(1, camera.Zoom);
            Assert.AreEqual(new Vector2(400, 300), camera.Position);
        }

        [TestMethod]
        public void GetTransformationTest()
        {
            //Arrange
            GraphicsDevice graphicsdevice = new(GraphicsAdapter.DefaultAdapter, GraphicsProfile.HiDef, new PresentationParameters());
            graphicsdevice.Viewport = new Viewport(0, 0, 800, 600);
            Mock<ITowerShooter> towershooter = new();
            towershooter.Setup(m => m.GraphicsDeviceManager.GraphicsDevice).Returns(graphicsdevice);
            Camera2D camera = new(towershooter.Object)
            {
                Zoom = 3
            };

            //Act
            Matrix result = camera.GetTransformation();

            //Assert
            Assert.AreEqual(new Matrix(3, 0, 0, 0, 0, 3, 0, 0, 0, 0, 1, 0, 400, 300, 0, 1), result);
        }

        [TestMethod]
        public void TestNegativeZoomClamp()
        {
            //Arrange
            Mock<ITowerShooter> towershooter = new();
            Camera2D camera = new(towershooter.Object)
            {
                Zoom = -1
            };

            //Act
            camera.Update();

            //Assert
            Assert.AreEqual(1, camera.Zoom);
        }

    }
}