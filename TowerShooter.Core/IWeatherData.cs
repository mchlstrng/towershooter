namespace TowerShooter
{
    public interface IWeatherData
    {
        int RainEndHour { get; set; }
        int RainStartHour { get; set; }
        void Update();
        void CalculateWeather(int howLongShouldItRain, int startHour);
    }
}