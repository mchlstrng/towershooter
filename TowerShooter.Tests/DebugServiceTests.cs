using Moq;

namespace TowerShooter.Tests
{
    [TestClass]
    public class DebugServiceTests
    {
        [DataTestMethod]
        [DataRow(true, true, false, DisplayName = "F3 is pressed, Debug is already true, Debug is false")]
        [DataRow(true, false, true, DisplayName = "F3 is pressed, Debug is already false, Debug is true")]
        [DataRow(false, true, true, DisplayName = "F3 is not pressed, Debug is already true, Debug is true")]
        [DataRow(false, false, false, DisplayName = "F3 is not pressed, Debug is already false, Debug is false")]
        public void DebugServiceTest(bool isSingleKeyPress, bool isAlreadyDebug, bool expectedValue)
        {
            //Arrange
            Mock<ITowerShooter> towershooterMock = new();
            towershooterMock.SetupAllProperties();
            towershooterMock.Object.Debug = isAlreadyDebug;
            towershooterMock.Setup(m => m.InputManager.IsSingleKeyPress(Microsoft.Xna.Framework.Input.Keys.F3)).Returns(isSingleKeyPress);
            DebugService service = new(towershooterMock.Object);

            //Act
            service.Update();

            //Assert
            Assert.AreEqual(expectedValue, towershooterMock.Object.Debug);
        }
    }
}