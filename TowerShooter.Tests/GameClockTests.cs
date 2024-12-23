using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace TowerShooter.Tests
{
    [TestClass]
    public class GameClockTests
    {
        private GameClock GetGameClockInstance(int startHour)
        {
            Mock<IWeatherData> weatherData = new();
            Mock<ITowerShooter> towerShooter = new();
            towerShooter.Setup(m => m.WeatherData).Returns(weatherData.Object);
            GameClock gameClock = new(towerShooter.Object);
            gameClock.SetStartHour(8);
            return gameClock;
        }

        [TestMethod]
        public void TestGameClock()
        {
            //Arrange
            GameClock gameClock = GetGameClockInstance(8);

            //Act
            TimeSpan time = TimeSpan.FromHours(0);
            gameClock.GameClockSeconds = (float)time.TotalSeconds;

            //Assert
            Assert.AreEqual(0, gameClock.Ambient);
        }

        [TestMethod]
        public void ConvertSecondsToHours()
        {
            //Arrange
            GameClock gameClock = GetGameClockInstance(8);

            //Act
            float result = gameClock.ConvertSecondsToHours(3600);

            //Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void GetCurrentGameClockSecondsAsTime()
        {
            //Arrange
            GameClock gameClock = GetGameClockInstance(8);

            //Act
            TimeSpan result = gameClock.GetCurrentGameClockSecondsAsTime();

            //Assert
            Assert.AreEqual(TimeSpan.FromHours(8), result);
        }

        [TestMethod]
        public void GetGameClockSecondsToTimeAsString()
        {
            //Arrange
            GameClock gameClock = GetGameClockInstance(8);

            //Act
            string result = gameClock.GetGameClockSecondsToTimeAsString();

            //Assert
            Assert.AreEqual("08:00:00", result);
        }

        [TestMethod]
        public void GameClock_NextDay()
        {
            //Arrange
            Microsoft.Xna.Framework.GameTime gameTime = new()
            {
                ElapsedGameTime = TimeSpan.FromSeconds(1)
            };

            GameClock gameClock = GetGameClockInstance(23);
            gameClock.GameClockSeconds = 86400;

            //Act
            gameClock.Update(gameTime);

            //Assert
            Assert.AreEqual(2, gameClock.CurrentDay);
            Assert.AreEqual(0.05f, gameClock.Ambient);
        }
    }
}