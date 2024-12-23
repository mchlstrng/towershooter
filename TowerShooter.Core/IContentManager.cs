namespace TowerShooter
{
    public interface IContentManager
    {
        T Load<T>(string assetName);
        ISpriteFont LoadSpriteFont(string spritefontName);
        ITexture2D LoadTexture2D(string spritefontName);
        ISoundEffect LoadSoundEffect(string soundEffectName);
    }
}