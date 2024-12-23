using Microsoft.Xna.Framework;

namespace TowerShooter
{
    /// <summary>
    /// Plays sound effects.
    /// </summary>
    public interface ISoundEffectPlayer
    {
        /// <summary>
        /// Plays a sound effect. The sound effect will only be played if the soundInterval has passed since the last time the sound effect was played.
        /// </summary>
        /// <param name="gameTime">
        /// The current game time.
        /// </param>
        /// <param name="soundEffect">
        /// The sound effect to play.
        /// </param>
        /// <param name="soundInterval">
        /// The interval between each time the sound effect is played.
        /// </param>
        void PlaySoundEffect(GameTime gameTime, ISoundEffect soundEffect, float soundInterval);
    }
}