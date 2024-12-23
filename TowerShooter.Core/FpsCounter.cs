using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace TowerShooter
{

    public class FpsCounter : IFpsCounter
    {
        readonly FramesPerSecondCounter _framesPerSecondCounter;
        public FpsCounter()
        {
            _framesPerSecondCounter = new FramesPerSecondCounter();
        }

        public int FramesPerSecond => _framesPerSecondCounter.FramesPerSecond;

        public void Draw(GameTime gameTime)
        {
            _framesPerSecondCounter.Draw(gameTime);
        }

        public void Update(GameTime gameTime)
        {
            _framesPerSecondCounter.Update(gameTime);
        }
    }

}