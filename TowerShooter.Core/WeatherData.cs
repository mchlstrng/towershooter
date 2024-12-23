using System;

namespace TowerShooter
{
    public class WeatherData : IWeatherData
    {
        private readonly ITowerShooter towerShooter;

        public int RainStartHour { get; set; }
        public int RainEndHour { get; set; }

        public WeatherData(ITowerShooter towerShooter)
        {
            this.towerShooter = towerShooter;
        }

        public void Update()
        {
            if (towerShooter.GameClock.GameClockSeconds > towerShooter.GameClock.ConvertHoursToSeconds(RainStartHour)
                && towerShooter.GameClock.GameClockSeconds < towerShooter.GameClock.ConvertHoursToSeconds(RainEndHour))
            {
                towerShooter.RainEngine.Enabled = true;
            }
            else
            {
                towerShooter.RainEngine.Enabled = false;
            }
        }

        public void CalculateWeather(int howLongShouldItRain, int startHour)
        {
            RainStartHour = startHour;
            RainEndHour = startHour + howLongShouldItRain;
            RainEndHour = Math.Clamp(value: RainEndHour, min: startHour, max: 24);

            //set random wind speed.
            Random rand = new();
            const double min = -0.025;
            const double max = 0.025;
            double range = max - min;
            double sample = rand.NextDouble();
            double scaled = (sample * range) + min;
            float f = (float)scaled;

            towerShooter.RainEngine.WindSpeed = f;
        }
    }
}