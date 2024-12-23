using TowerShooter.Screens;

namespace TowerShooter.Core.Tests.Screens
{
    [TestClass]
    public class SettingItemTests
    {
        [TestMethod]
        public void Test_SetNextSettingValue_FirstIteration()
        {
            // Arrange
            var settingItem = new SettingItem
            {
                Name = "TestSetting",
                ValueIndex = 0,
                AvailableValues = new List<string> { "Value1", "Value2", "Value3" }
            };

            // Act
            settingItem.SetNextSettingValue();

            // Assert
            Assert.AreEqual(1, settingItem.ValueIndex);
            Assert.AreEqual("Value2", settingItem.CurrentValue);
        }

        [TestMethod]
        public void Test_SetNextSettingValue_SecondIteration()
        {
            // Arrange
            var settingItem = new SettingItem
            {
                Name = "TestSetting",
                ValueIndex = 1,
                AvailableValues = new List<string> { "Value1", "Value2", "Value3" }
            };

            // Act
            settingItem.SetNextSettingValue();

            // Assert
            Assert.AreEqual(2, settingItem.ValueIndex);
            Assert.AreEqual("Value3", settingItem.CurrentValue);
        }

        [TestMethod]
        public void Test_SetNextSettingValue_ThirdIteration()
        {
            // Arrange
            var settingItem = new SettingItem
            {
                Name = "TestSetting",
                ValueIndex = 2,
                AvailableValues = new List<string> { "Value1", "Value2", "Value3" }
            };

            // Act
            settingItem.SetNextSettingValue();

            // Assert
            Assert.AreEqual(0, settingItem.ValueIndex);
            Assert.AreEqual("Value1", settingItem.CurrentValue);
        }
    }
}