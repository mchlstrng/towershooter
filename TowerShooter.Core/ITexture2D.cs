using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerShooter
{

    public interface ITexture2D
    {
        int Height { get; }
        int Width { get; }
        Texture2D GetTexture2D();
    }

}