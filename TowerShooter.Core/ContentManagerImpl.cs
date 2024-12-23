using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TowerShooter
{
    public class ContentManagerImpl : IContentManager
    {
        private readonly ContentManager contentManager;

        public ContentManagerImpl(ContentManager contentManager)
        {
            this.contentManager = contentManager;
        }

        public T Load<T>(string assetName)
        {
            return contentManager.Load<T>(assetName);
        }

        public ISoundEffect LoadSoundEffect(string soundEffectName)
        {
            SoundEffect soundEffect = Load<SoundEffect>(soundEffectName);
            return new SoundEffectImpl(soundEffect);
        }

        public ISpriteFont LoadSpriteFont(string spritefontName)
        {
            SpriteFont spriteFont = Load<SpriteFont>(spritefontName);
            return new SpriteFontImpl(spriteFont);
        }

        public ITexture2D LoadTexture2D(string spritefontName)
        {
            Texture2D texture2D = Load<Texture2D>(spritefontName);
            return new Texture2DImpl(texture2D);
        }
    }
}
