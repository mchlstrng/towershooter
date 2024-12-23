namespace TowerShooter.Tests
{
    [TestClass()]
    public class KeyboardStateImplTests
    {
        [TestMethod()]
        public void GetStateTest()
        {
            //Arrange
            KeyboardStateImpl keyboardStateImpl = new();

            //Act
            Microsoft.Xna.Framework.Input.KeyboardState result = keyboardStateImpl.GetState();

            //Assert
            Assert.IsNotNull(result);
        }
    }
}