using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerShooter
{
    public class SpriteFontImpl : ISpriteFont
    {
        private readonly SpriteFont spriteFont;

        public SpriteFontImpl(SpriteFont spriteFont)
        {
            this.spriteFont = spriteFont;
        }

        public SpriteFont GetSpriteFont()
        {
            return spriteFont;
        }

        public Vector2 MeasureString(string text)
        {
            return spriteFont.MeasureString(text);
        }
    }
}
