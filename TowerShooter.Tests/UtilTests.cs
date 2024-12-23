using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Moq;
using System.Collections.Generic;
using System.Linq;
using TowerShooter.Blocks;
using TowerShooter.Screens;

namespace TowerShooter.Tests
{
    [TestClass]
    public class UtilTests
    {
        [TestMethod]
        public void CalculateDiagonalDistance()
        {
            //Arrange
            Vector2 pos1 = new(10, 10);
            Vector2 pos2 = new(20, 10);

            //Act
            float result = Util.CalculateDiagonalDistance(pos1, pos2);

            //Assert
            Assert.AreEqual(10, result);
        }

        [TestMethod]
        public void ConvertOneRangeNumberToAnother()
        {
            //Arrange

            const float oldValue = 1;

            const float oldMin = 1;
            const float oldMax = 5;
            const float newMin = 2;
            const float newMax = 10;

            //Act
            float result = Util.ConvertOneRangeNumberToAnother(oldValue, oldMin, oldMax, newMin, newMax);

            //Assert
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void ConvertOneRangeNumberToAnother_2()
        {
            //Arrange

            const float oldValue = -5;

            const float oldMin = -5;
            const float oldMax = 5;
            const float newMin = 0;
            const float newMax = 10;

            //Act
            float result = Util.ConvertOneRangeNumberToAnother(oldValue, oldMin, oldMax, newMin, newMax);

            //Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void ConvertOneRangeNumberToAnother_3()
        {
            //Arrange

            const float oldValue = 5;

            const float oldMin = -5;
            const float oldMax = 5;
            const float newMin = 0;
            const float newMax = 10;

            //Act
            float result = Util.ConvertOneRangeNumberToAnother(oldValue, oldMin, oldMax, newMin, newMax);

            //Assert
            Assert.AreEqual(10, result);
        }

        [TestMethod]
        public void ConvertOneRangeNumberToAnother_4()
        {
            //Arrange

            const float oldValue = 0;

            const float oldMin = -5;
            const float oldMax = 5;
            const float newMin = 0;
            const float newMax = 10;

            //Act
            float result = Util.ConvertOneRangeNumberToAnother(oldValue, oldMin, oldMax, newMin, newMax);

            //Assert
            Assert.AreEqual(5, result);
        }

        [TestMethod]
        public void CalculateSineWaveForTime()
        {
            //Arrange + Act + Assert
            Assert.AreEqual(-0.999999642372131f, Util.CalculateSineWaveForTime(currentTimeInSeconds: 12, wholeDayInSeconds: 86400));
            Assert.AreEqual(-1, Util.CalculateSineWaveForTime(currentTimeInSeconds: 0, wholeDayInSeconds: 86400));
        }


        [TestMethod]
        public void GetBlocksInCollisionLine()
        {
            //Arrange
            List<IGameBlock> blocks = new();
            //[PLAYER]-----[BLOCK]-----[SOLIDBLOCK]-----[BLOCK]
            //line does not hit the last block because there is a solid block between
            blocks.Add(CreateGameBlock(new Vector2(10, 10)));
            blocks.Add(CreateSolidGameBlock(new Vector2(17, 10)));
            blocks.Add(CreateGameBlock(new Vector2(20, 10)));

            Vector2 p1 = new(10, 10);
            Vector2 p2 = new(20, 10);
            const int stepSize = 1;

            //Act
            CollisionLineResult result = Util.GetBlocksInCollisionLine(p1, p2, stepSize, blocks, true);

            //Assert
            Assert.AreEqual(10, result.DiagonalDistance);
            Assert.AreEqual(2, result.Blocks.Count);
            Assert.AreEqual(new Vector2(20, 10), result.Blocks[0].GetPosition());
            Assert.AreEqual(new Vector2(17, 10), result.Blocks[1].GetPosition());
        }

        private IGameBlock CreateGameBlock(Vector2 pos)
        {
            Mock<IGameBlock> block = new();
            block.Setup(m => m.GetPosition()).Returns(pos);
            block.SetupAllProperties();
            return block.Object;
        }

        private IGameBlock CreateSolidGameBlock(Vector2 pos)
        {
            Mock<IGameBlock> block = new();
            block.SetupAllProperties();
            block.Setup(m => m.GetPosition()).Returns(pos);
            block.Setup(m => m.IsSolid).Returns(true);
            return block.Object;
        }

        [TestMethod]
        public void RectangleToBoundingBox()
        {
            //Arrange
            Rectangle rect = new();
            rect.Height = 5;
            rect.Width = 20;
            rect.X = 15;
            rect.Y = 25;

            //Act
            BoundingBox bBox = Util.RectangleToBoundingBox(rect);

            //Assert
            Assert.AreEqual(new Vector3(15, 25, 0), bBox.Min);
            Assert.AreEqual(new Vector3(35, 30, 0), bBox.Max);
        }

        [TestMethod]
        public void GetBlockAbove()
        {
            //Arrange
            List<IGameBlock> blocks = new();
            blocks.Add(CreateGameBlock(new Vector2(10, 10)));
            blocks.Add(CreateGameBlock(new Vector2(10, 20)));
            blocks.Add(CreateGameBlock(new Vector2(10, 30)));

            Vector2 pos = new(10, 20);

            const int blockSize = 10;

            //Act
            IGameBlock result = Util.GetBlockAbove(pos, blockSize, blocks);

            //Assert
            Assert.AreEqual(new Vector2(10, 10), result.GetPosition());
        }

        [TestMethod]
        public void GetBlockBelow()
        {
            //Arrange
            List<IGameBlock> blocks = new();
            blocks.Add(CreateGameBlock(new Vector2(10, 10)));
            blocks.Add(CreateGameBlock(new Vector2(10, 20)));
            blocks.Add(CreateGameBlock(new Vector2(10, 30)));

            Vector2 pos = new(10, 20);

            const int blockSize = 10;

            //Act
            IGameBlock result = Util.GetBlockBelow(pos, blockSize, blocks);

            //Assert
            Assert.AreEqual(new Vector2(10, 30), result.GetPosition());
        }

        [TestMethod]
        public void GetBlockToTheRight()
        {
            //Arrange
            List<IGameBlock> blocks = new();
            blocks.Add(CreateGameBlock(new Vector2(10, 10)));
            blocks.Add(CreateGameBlock(new Vector2(20, 10)));

            Vector2 pos = new(10, 10);

            const int blockSize = 10;

            //Act
            IGameBlock result = Util.GetBlockToTheRight(pos, blockSize, blocks);

            //Assert
            Assert.AreEqual(new Vector2(20, 10), result.GetPosition());
        }

        [TestMethod]
        public void GetBlockToTheLeft()
        {
            //Arrange
            List<IGameBlock> blocks = new();
            blocks.Add(CreateGameBlock(new Vector2(10, 10)));
            blocks.Add(CreateGameBlock(new Vector2(20, 10)));

            Vector2 pos = new(20, 10);

            const int blockSize = 10;

            //Act
            IGameBlock result = Util.GetBlockToTheLeft(pos, blockSize, blocks);

            //Assert
            Assert.AreEqual(new Vector2(10, 10), result.GetPosition());
        }

    }
}