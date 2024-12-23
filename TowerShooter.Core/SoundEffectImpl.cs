using Microsoft.Xna.Framework.Audio;

namespace TowerShooter
{
    public class SoundEffectImpl : ISoundEffect
    {
        private readonly SoundEffect soundEffect;
        private readonly SoundEffectInstance soundEffectInstance;

        public SoundEffectImpl(SoundEffect soundEffect)
        {
            this.soundEffect = soundEffect;
            soundEffectInstance = soundEffect.CreateInstance();
        }

        public SoundEffect GetSoundEffect()
        {
            return soundEffect;
        }

        public void Play()
        {
            if (soundEffectInstance.State != SoundState.Playing)
                soundEffectInstance.Play();
        }
    }
}
