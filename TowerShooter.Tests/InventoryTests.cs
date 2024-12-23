using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace TowerShooter.Tests
{
    [TestClass]
    public class InventoryTests
    {
        [TestMethod]
        public void AddToInventoryAndGetItemsBack()
        {
            //Arrange

            Mock<ITowerShooter> towershooter = new();
            towershooter.Setup(m => m.BlockTypeToTextureService.GetTexture(BlockType.Stone)).Returns(new Mock<ITexture2D>().Object);
            Inventory inventory = new(towershooter.Object);
            InventoryItem inventoryItem = new()
            {
                BlockType = BlockType.Stone,
                Amount = 1
            };

            //Act
            inventory.Add(inventoryItem);
            List<InventoryItem> inventoryItems = inventory.GetInventory();

            //Assert
            Assert.AreEqual(BlockType.Stone, inventoryItems[0].BlockType);
            Assert.AreEqual(1, inventoryItems[0].Amount);
        }

        [TestMethod]
        public void AddToExisting()
        {
            //Arrange
            Mock<ITowerShooter> towershooter = new();
            towershooter.Setup(m => m.BlockTypeToTextureService.GetTexture(BlockType.Stone)).Returns(new Mock<ITexture2D>().Object);

            Inventory inventory = new(towershooter.Object);
            inventory.Add(new InventoryItem
            {
                Amount = 1,
                BlockType = BlockType.Stone
            });

            //Act
            inventory.AddToExisting(BlockType.Stone);

            //Assert
            Assert.AreEqual(2, inventory.GetActiveInventoryItem().Amount);
        }

        [TestMethod]
        public void AddToExistingNoExistingBlockTypeInInventory()
        {
            //Arrange
            Mock<ITowerShooter> towershooter = new();
            towershooter.Setup(m => m.BlockTypeToTextureService.GetTexture(BlockType.Stone)).Returns(new Mock<ITexture2D>().Object);

            Inventory inventory = new(towershooter.Object);

            //Act
            inventory.AddToExisting(BlockType.Stone);

            //Assert
            Assert.AreEqual(1, inventory.GetActiveInventoryItem().Amount);
        }

        [TestMethod]
        public void RemoveFromExisting()
        {
            //Arrange
            Mock<ITowerShooter> towershooter = new();
            towershooter.Setup(m => m.BlockTypeToTextureService.GetTexture(BlockType.Stone)).Returns(new Mock<ITexture2D>().Object);

            Inventory inventory = new(towershooter.Object);
            inventory.Add(new InventoryItem
            {
                Amount = 2,
                BlockType = BlockType.Stone
            });

            //Act
            inventory.RemoveItem(BlockType.Stone);

            //Assert
            Assert.AreEqual(1, inventory.GetActiveInventoryItem().Amount);
        }


        [TestMethod]
        public void RemoveLastFromExistingAssertThatItReturnsNull()
        {
            //Arrange
            Mock<ITowerShooter> towershooter = new();
            towershooter.Setup(m => m.BlockTypeToTextureService.GetTexture(BlockType.Stone)).Returns(new Mock<ITexture2D>().Object);

            Inventory inventory = new(towershooter.Object);
            inventory.Add(new InventoryItem
            {
                Amount = 1,
                BlockType = BlockType.Stone
            });

            //Act
            inventory.RemoveItem(BlockType.Stone);

            //Assert
            Assert.AreEqual(null, inventory.GetActiveInventoryItem());
        }

    }
}