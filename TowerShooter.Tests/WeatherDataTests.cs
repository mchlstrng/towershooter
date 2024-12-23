using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace TowerShooter.Tests
{
    [TestClass]
    public class WeatherDataTests
    {
        class ServiceWithMocks
        {
            public WeatherData WeatherData { get; set; }
            public Mock<ITowerShooter> TowerShooter { get; set; }
        }

        ServiceWithMocks GetServiceWithMocks()
        {
            Mock<IRainEngine> rainEngine = new();
            Mock<ITowerShooter> towershooter = new();
            towershooter.Setup(m => m.RainEngine).Returns(rainEngine.Object);
            WeatherData weatherData = new(towershooter.Object);

            return new ServiceWithMocks
            {
                TowerShooter = towershooter,
                WeatherData = weatherData
            };
        }

        [TestMethod]
        public void CalculateWeather_Start_8_Rain_12_Hours()
        {
            //Arrange
            ServiceWithMocks serviceWithMocks = GetServiceWithMocks();

            //Act
            serviceWithMocks.WeatherData.CalculateWeather(howLongShouldItRain: 12, startHour: 8);

            //Assert            
            Assert.AreEqual(8, serviceWithMocks.WeatherData.RainStartHour);
            Assert.AreEqual(20, serviceWithMocks.WeatherData.RainEndHour);
        }

        [TestMethod]
        public void CalculateWeather_Start_24_Rain_1_Hours()
        {
            //Arrange
            ServiceWithMocks serviceWithMocks = GetServiceWithMocks();

            //Act
            serviceWithMocks.WeatherData.CalculateWeather(howLongShouldItRain: 1, startHour: 24);

            //Assert            
            Assert.AreEqual(24, serviceWithMocks.WeatherData.RainStartHour);
            Assert.AreEqual(24, serviceWithMocks.WeatherData.RainEndHour);
        }

        [TestMethod]
        public void CalculateWeather_Start_20_Rain_6_Hours()
        {
            //Arrange
            ServiceWithMocks serviceWithMocks = GetServiceWithMocks();

            //Act
            serviceWithMocks.WeatherData.CalculateWeather(howLongShouldItRain: 6, startHour: 20);

            //Assert            
            Assert.AreEqual(20, serviceWithMocks.WeatherData.RainStartHour);
            Assert.AreEqual(24, serviceWithMocks.WeatherData.RainEndHour);
        }

        [TestMethod]
        public void Test_Update_Below_Rain_Start()
        {
            //Arrange

            //Act

            //Assert
        }

    }
}