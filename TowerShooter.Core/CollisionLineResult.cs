using System.Collections.Generic;
using TowerShooter.Blocks;

namespace TowerShooter
{
    public class CollisionLineResult
    {
        public List<IGameBlock> Blocks { get; set; }
        public float DiagonalDistance { get; set; }
    }
}
