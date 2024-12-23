using Microsoft.Xna.Framework.Graphics;

namespace TowerShooter
{
    public interface IBlockTypeToTextureService
    {
        ITexture2D GetTexture(BlockType blockType);
    }
}