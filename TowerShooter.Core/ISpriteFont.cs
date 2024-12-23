using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerShooter
{
    public interface ISpriteFont
    {
        Vector2 MeasureString(string text);
        SpriteFont GetSpriteFont();
    }

}