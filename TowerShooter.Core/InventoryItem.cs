using Microsoft.Xna.Framework.Graphics;

namespace TowerShooter
{
    public class InventoryItem
    {
        public BlockType BlockType { get; set; }
        public int Amount { get; set; }
        public ITexture2D Texture { get; set; }
    }
}