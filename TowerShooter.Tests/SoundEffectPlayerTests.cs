using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Moq;

namespace TowerShooter.Tests
{
    [TestClass]
    public class SoundEffectPlayerTests
    {
        [TestMethod]
        public void PlaySoundEffectTest_ElapsedGameTime_IsEqualToSoundInterval()
        {
            //Arrange
            GameTime gameTime = new()
            {
                ElapsedGameTime = new TimeSpan(0, 0, 0, 0, 1000)
            };

            Mock<ISoundEffect> soundEffect = new();
            const float soundInterval = 1f;

            SoundEffectPlayer soundEffectPlayer = new();

            //Act
            soundEffectPlayer.PlaySoundEffect(gameTime, soundEffect.Object, soundInterval);

            //Assert
            soundEffect.Verify(x => x.Play(), Times.Exactly(2));
        }

        [TestMethod]
        public void PlaySoundEffectTest_ElapsedGameTime_IsNotEqualToSoundInterval()
        {
            //Arrange
            GameTime gameTime = new()
            {
                ElapsedGameTime = new TimeSpan(0, 0, 0, 0, 500)
            };

            Mock<ISoundEffect> soundEffect = new();
            const float soundInterval = 1f;

            SoundEffectPlayer soundEffectPlayer = new();

            //Act
            soundEffectPlayer.PlaySoundEffect(gameTime, soundEffect.Object, soundInterval);

            //Assert
            soundEffect.Verify(x => x.Play(), Times.Once);
        }

        /// <summary>
        /// Test method where the elapsedtime is 0, and assert that the soundeffect is played once.
        /// </summary>
        [TestMethod]
        public void PlaySoundEffectTest_ElapsedTime_0_Assert_Played_Once()
        {
            //Arrange
            GameTime gameTime = new()
            {
                ElapsedGameTime = new TimeSpan(0, 0, 0, 0, 0)
            };
            Mock<ISoundEffect> soundEffect = new();
            const float soundInterval = 1f;
            SoundEffectPlayer soundEffectPlayer = new();
            //Act
            soundEffectPlayer.PlaySoundEffect(gameTime, soundEffect.Object, soundInterval);
            //Assert
            soundEffect.Verify(x => x.Play(), Times.Once);
        }
    }
}