using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace TowerShooter
{
    public class Inventory : IInventory
    {
        public Inventory(ITowerShooter towerShooter)
        {
            this.towerShooter = towerShooter;
        }

        private readonly List<InventoryItem> InventoryItems = new();

        private int activeInventoryIndex;

        public bool InventoryVisible { get; set; }

        private readonly ITowerShooter towerShooter;

        /// <summary>
        /// Add an item to the inventory
        /// </summary>
        /// <param name="inventoryItem">Item to add</param>
        public void Add(InventoryItem inventoryItem)
        {
            InventoryItems.Add(inventoryItem);

            inventoryItem.Texture = towerShooter.BlockTypeToTextureService.GetTexture(inventoryItem.BlockType);
        }

        /// <summary>
        /// Get the entire inventory
        /// </summary>
        /// <returns></returns>
        public List<InventoryItem> GetInventory()
        {
            return InventoryItems;
        }

        public InventoryItem GetActiveInventoryItem()
        {
            if (activeInventoryIndex >= 0 && activeInventoryIndex < InventoryItems.Count)
            {
                //return item if index is inside bounds of the array
                return InventoryItems[activeInventoryIndex];
            }
            else
            {
                //else return null
                return null;
            }
        }

        public void AddToExisting(BlockType blockType)
        {
            //check if item is already in inventory
            InventoryItem existingItem = InventoryItems.Find(i => i.BlockType == blockType);
            if (existingItem != null)
            {
                //if item is already in inventory, add to amount
                existingItem.Amount++;
            }
            else
            {
                //if item is not in inventory, add it
                Add(new InventoryItem
                {
                    BlockType = blockType,
                    Amount = 1
                });
            }
        }

        public void GoToNextInventoryItem()
        {
            activeInventoryIndex++;
            ClampInventoryIndex();
        }

        public void GoToPreviousInventoryItem()
        {
            activeInventoryIndex--;
            ClampInventoryIndex();
        }

        private void ClampInventoryIndex()
        {
            activeInventoryIndex = MathHelper.Clamp(activeInventoryIndex, 0, InventoryItems.Count - 1);
        }

        /// <summary>
        /// Remove an item from the inventory
        /// </summary>
        /// <param name="blockType"></param>
        public void RemoveItem(BlockType blockType)
        {
            foreach (InventoryItem item in GetInventory())
            {
                if (item.BlockType == blockType && item.Amount >= 1)
                {
                    item.Amount--;
                    //remove if amount is 0
                    if (item.Amount == 0)
                    {
                        InventoryItems.Remove(item);
                    }
                    break;
                }
            }
        }

        public void ToggleInventory()
        {
            InventoryVisible = !InventoryVisible;
        }

        public void Draw(ISpriteBatch spriteBatch, GameTime gameTime)
        {
            int xPos = 100;
            int yPos = 100;
            const int xPadding = 20;
            int maxXpos = towerShooter.ResolutionX - xPadding;

            foreach (InventoryItem item in InventoryItems)
            {
                spriteBatch.Draw(item.Texture.GetTexture2D(), new Vector2(xPos, yPos), Color.White);
                xPos += item.Texture.GetTexture2D().Width + 20;

                if (xPos > maxXpos)
                {
                    xPos = 100;
                    yPos += item.Texture.GetTexture2D().Height + 20;
                }
            }
        }
    }
}