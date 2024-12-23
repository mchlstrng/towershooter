using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace TowerShooter
{
    public interface IInventory
    {
        bool InventoryVisible { get; set; }

        void Add(InventoryItem inventoryItem);
        void AddToExisting(BlockType blockType);
        void Draw(ISpriteBatch spriteBatch, GameTime gameTime);
        InventoryItem GetActiveInventoryItem();
        List<InventoryItem> GetInventory();
        void GoToNextInventoryItem();
        void GoToPreviousInventoryItem();
        void RemoveItem(BlockType blockType);
        void ToggleInventory();
    }
}