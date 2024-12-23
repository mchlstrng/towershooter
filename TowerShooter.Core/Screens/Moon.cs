using Microsoft.Xna.Framework;
using System;

namespace TowerShooter.Screens
{
    public class Moon
    {
        private readonly ITexture2D texture2D;

        private Vector2 position;

        private readonly int startX;
        private readonly int endX;

        /// <summary>
        /// where on the screen is the horizon
        /// </summary>
        private readonly float horizonY;

        //how high on the horizon should the sun rise
        private readonly float amplitudeY;
        private readonly ITowerShooter towerShooter;

        public Moon(ITowerShooter towerShooter)
        {
            texture2D = towerShooter.ContentManager.LoadTexture2D("Moon");
            position = new Vector2(100, 100);

            startX = 0;
            endX = towerShooter.MapWidth - texture2D.Width;
            horizonY = towerShooter.MapHeight;
            amplitudeY = towerShooter.MapWidth;
            this.towerShooter = towerShooter;
        }

        public void Update(GameTime gameTime)
        {
            const int newMax = 1;

            float moonHours = towerShooter.GameClock.ConvertSecondsToHours(towerShooter.GameClock.MoonClockSeconds);

            float movVal = Util.ConvertOneRangeNumberToAnother(oldValue: moonHours, oldMin: 0f, oldMax: 8f, newMin: 0, newMax: newMax);

            float t = movVal / (newMax * 2) * MathHelper.TwoPi;

            float horizontal = (float)((1 + Math.Cos(t)) / 2f);
            float vertical = (float)-Math.Sin(t);

            float xPos = startX + ((endX - startX) * horizontal);
            float yPos = horizonY + (amplitudeY * vertical);

            position = new Vector2(xPos, yPos);
        }

        public void Draw(ISpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.BeginCameraZoom();
            spriteBatch.Draw(texture2D.GetTexture2D(), position, Color.White);
            spriteBatch.End();
        }
    }
}