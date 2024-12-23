using Microsoft.Xna.Framework.Graphics;

namespace TowerShooter
{
    internal class Texture2DImpl : ITexture2D
    {
        private readonly Texture2D texture2D;

        public Texture2DImpl(Texture2D texture2D)
        {
            this.texture2D = texture2D;
        }

        public int Height => texture2D.Height;

        public int Width => texture2D.Width;

        public Texture2D GetTexture2D()
        {
            return texture2D;
        }
    }
}