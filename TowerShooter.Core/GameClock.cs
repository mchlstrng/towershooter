using Microsoft.Xna.Framework;
using System;
using System.Linq;
using TowerShooter.Blocks;

namespace TowerShooter
{

    public class GameClock : IGameClock
    {
        public float Ambient { get; set; }
        public float GameClockSeconds { get; set; }
        public float MoonClockSeconds { get; set; }
        public float SpawnHpClockSeconds { get; set; }

        public long CurrentDay { get; set; }

        private readonly ITowerShooter towerShooter;

        public GameClock(ITowerShooter towerShooter)
        {
            this.towerShooter = towerShooter;
        }

        public void SetStartHour(int startHour)
        {
            CurrentDay = 1;
            GameClockSeconds = ConvertHoursToSeconds(startHour);
        }

        public float ConvertHoursToSeconds(int hours)
        {
            TimeSpan time = TimeSpan.FromHours(hours);
            return (float)time.TotalSeconds;
        }

        public float ConvertSecondsToHours(float seconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(seconds);
            return (float)time.TotalHours;
        }

        public void Update(GameTime gameTime)
        {
            GameClockSeconds += (float)gameTime.ElapsedGameTime.TotalSeconds * 60;

            float currentGameClockHour = ConvertSecondsToHours(GameClockSeconds);

            if (currentGameClockHour == 22)
            {
                MoonClockSeconds = 0;
            }
            else if (currentGameClockHour >= 22 || currentGameClockHour <= 6)
            {
                MoonClockSeconds += (float)gameTime.ElapsedGameTime.TotalSeconds * 60;
            }
            else
            {
                MoonClockSeconds = 0;
            }

            //24 hours = 86400 seconds
            //reset day of over max seconds per day
            if (GameClockSeconds > ConvertHoursToSeconds(24))
            {
                GameClockSeconds = 0;
                CurrentDay++;
                towerShooter.WeatherData.CalculateWeather(howLongShouldItRain: new Random().Next(1, 23), startHour: new Random().Next(0, 25));
            }

            //Converting time of day into a smooth day/night variable
            //https://gamedev.stackexchange.com/questions/84135/converting-time-of-day-into-a-smooth-day-night-variable?noredirect=1

            float sineAmbient = Util.CalculateSineWaveForTime(GameClockSeconds, ConvertHoursToSeconds(24));

            Ambient = Util.ConvertOneRangeNumberToAnother(
                sineAmbient,
                oldMin: -1,
                oldMax: 1,
                newMin: 0.05f,
                newMax: 1);

            towerShooter.WeatherData.Update();


            if (SpawnHpClockSeconds > 5 && towerShooter.GameplayGameScreen.Blocks.Exists(b => b.BlockType == BlockType.HealthPack) == false)
            {
                SpawnHpClockSeconds = 0;
                //get spawn points and spawn hp at random position
                System.Collections.Generic.List<IGameBlock> blocksWhereItemsCanSpawn = towerShooter.GameplayGameScreen.Blocks.Where(b => b.ItemCanSpawnOnTop && Util.GetBlockAbove(b.GetPosition(), (int)b.GetSize().X, towerShooter.GameplayGameScreen.Blocks).BlockType == BlockType.Air).ToList();

                int randomInt = new Random().Next(blocksWhereItemsCanSpawn.Count);

                IGameBlock block = blocksWhereItemsCanSpawn[randomInt];

                //remove airblock above
                IGameBlock airBlockToReplaceWithHealthPackBlock = Util.GetBlockAbove(block.GetPosition(), (int)block.GetSize().X, towerShooter.GameplayGameScreen.Blocks);

                //insert hp block
                towerShooter.GameplayGameScreen.Blocks.Remove(airBlockToReplaceWithHealthPackBlock);
                towerShooter.GameplayGameScreen.Blocks.Add(new HealthPackBlock((int)airBlockToReplaceWithHealthPackBlock.GetPosition().X, (int)airBlockToReplaceWithHealthPackBlock.GetPosition().Y, towerShooter));
            }
            else
            {
                SpawnHpClockSeconds += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

        }

        public TimeSpan GetCurrentGameClockSecondsAsTime()
        {
            TimeSpan time = TimeSpan.FromSeconds(GameClockSeconds);
            return time;
        }

        public string GetGameClockSecondsToTimeAsString()
        {
            string time = GetCurrentGameClockSecondsAsTime().ToString(@"hh\:mm\:ss");
            return time;
        }
    }
}
