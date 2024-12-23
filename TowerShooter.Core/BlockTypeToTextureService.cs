using System;

namespace TowerShooter
{
    public class BlockTypeToTextureService : IBlockTypeToTextureService
    {
        private readonly ITowerShooter towerShooter;

        public BlockTypeToTextureService(ITowerShooter towerShooter)
        {
            this.towerShooter = towerShooter;
        }

        public ITexture2D GetTexture(BlockType blockType)
        {
            switch (blockType)
            {
                case BlockType.Air:
                    return towerShooter.ContentManager.LoadTexture2D(Texture2dStringNames.AirBlock);
                case BlockType.Stone:
                    return towerShooter.ContentManager.LoadTexture2D(Texture2dStringNames.SolidBlock);
                case BlockType.Ladder:
                    break;
                case BlockType.MouseOver:
                    break;
                case BlockType.Wood:
                    return towerShooter.ContentManager.LoadTexture2D(Texture2dStringNames.WoodBlock);
            }

            throw new Exception("Could not get texture for blocktype: " + blockType);

        }
    }
}