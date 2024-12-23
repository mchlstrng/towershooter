using Microsoft.Xna.Framework;
using System;

namespace TowerShooter
{
    public interface IGameClock
    {
        float Ambient { get; set; }
        float GameClockSeconds { get; set; }
        long CurrentDay { get; set; }
        float MoonClockSeconds { get; set; }
        float SpawnHpClockSeconds { get; set; }

        float ConvertHoursToSeconds(int hours);
        float ConvertSecondsToHours(float seconds);
        TimeSpan GetCurrentGameClockSecondsAsTime();
        string GetGameClockSecondsToTimeAsString();
        void Update(GameTime gameTime);
        void SetStartHour(int startHour);
    }
}