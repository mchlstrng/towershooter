using Microsoft.Xna.Framework;

namespace TowerShooter
{
    public class SoundEffectPlayer : ISoundEffectPlayer
    {
        private float elapsedTime;

        public void PlaySoundEffect(GameTime gameTime, ISoundEffect soundEffect, float soundInterval)
        {
            //play the effect the first time it is called.
            if (elapsedTime == 0)
            {
                soundEffect.Play();
            }

            //increment the elapsed time by the number of seconds that have elapsed since the last time that the update method was called.
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            //if the elapsed time is greater than the specified interval
            if (elapsedTime >= soundInterval)
            {
                //reset the elapsed time.
                elapsedTime = 0;
                //play the sound effect.
                soundEffect.Play();
            }
        }
    }
}