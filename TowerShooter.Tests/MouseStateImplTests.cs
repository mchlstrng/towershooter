namespace TowerShooter.Tests
{
    [TestClass()]
    public class MouseStateImplTests
    {
        [TestMethod()]
        public void GetStateTest()
        {
            //Arrange
            MouseStateImpl mouseState = new();

            //Act
            Microsoft.Xna.Framework.Input.MouseState actual = mouseState.GetState();

            //Assert
            Assert.IsNotNull(actual);
        }
    }
}