using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TowerShooter.Blocks;

namespace TowerShooter
{
    public static class Util
    {
        public static float ConvertOneRangeNumberToAnother(float oldValue, float oldMin, float oldMax, float newMin, float newMax)
        {
            float OldRange = oldMax - oldMin;
            float NewRange = newMax - newMin;
            return (float)(((oldValue - oldMin) * NewRange / OldRange) + newMin);
        }

        public static float CalculateSineWaveForTime(float currentTimeInSeconds, float wholeDayInSeconds)
        {
            return (float)Math.Sin((-Math.PI / 2) + (2 * Math.PI * currentTimeInSeconds / wholeDayInSeconds));
        }

        public static BoundingBox RectangleToBoundingBox(Rectangle rectangle)
        {
            return new BoundingBox
            {
                Min = new Vector3(rectangle.X, rectangle.Y, 0),
                Max = new Vector3(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height, 0)
            };
        }

        /// <summary>
        /// Returns the blocks in a line from p0 to p1. If breakOnWallHit is true, the line will stop when it hits a solid block. If false, it will continue until the end of the line.
        /// </summary>
        /// <param name="p0">
        /// The start position of the line.
        /// </param>
        /// <param name="p1">
        /// The end position of the line.
        /// </param>
        /// <param name="stepSize">
        /// The step size of the line. The smaller the step size, the more accurate the line will be. However, the more blocks will be checked.
        /// </param>
        /// <param name="blocks">
        /// The blocks to check.
        /// </param>
        /// <param name="breakOnWallHit">
        /// If true, the line will stop when it hits a solid block. If false, it will continue until the end of the line.
        /// </param>
        /// <returns>
        /// A CollisionLineResult object containing the blocks in the line and the diagonal distance between p0 and p1.
        /// </returns>
        public static CollisionLineResult GetBlocksInCollisionLine(Vector2 p0, Vector2 p1, int stepSize, List<IGameBlock> blocks, bool breakOnWallHit)
        {
            CollisionLineResult result = new();

            List<IGameBlock> blocksInLine = new();

            if (p0 != default && p1 != default)
            {
                // Not Bresenham's line algorithm, but a much simpler algorithm that works for our purposes.
                //https://www.redblobgames.com/grids/line-drawing.html

                float diagonalDistance = CalculateDiagonalDistance(p0, p1);
                for (int step = 0; step <= diagonalDistance; step += stepSize)
                {
                    float t;
                    if (diagonalDistance == 0)
                    {
                        t = 0.0f;
                    }
                    else
                    {
                        t = step / diagonalDistance;
                    }

                    Vector2 pos = RoundVector2ForBlocks(LerpVector2(p1, p0, t), stepSize);

                    IGameBlock blockWithPos = blocks.SingleOrDefault(b => b.GetPosition().X == pos.X
                    && b.GetPosition().Y == pos.Y);

                    if (blockWithPos != null)
                    {
                        blocksInLine.Add(blockWithPos);

                        if (blockWithPos.IsSolid && breakOnWallHit)
                            break;
                    }
                }
                result.DiagonalDistance = diagonalDistance;
            }
            result.Blocks = blocksInLine;

            return result;
        }

        /// <summary>
        /// Calculates the diagonal distance between two vectors.
        /// </summary>
        /// <param name="vector1">
        /// The first vector.
        /// </param>
        /// <param name="vector2">
        /// The second vector.
        /// </param>
        /// <returns>
        /// The diagonal distance between the two vectors.
        /// </returns>
        public static float CalculateDiagonalDistance(Vector2 vector1, Vector2 vector2)
        {
            float dx = vector2.X - vector1.X;
            float dy = vector2.Y - vector1.Y;
            return (float)MathHelper.Max(Math.Abs(dx), Math.Abs(dy));
        }

        public static Vector2 RoundVector2ForBlocks(Vector2 vector2, int blockSize)
        {
            return new Vector2((float)Math.Round(vector2.X / blockSize) * blockSize, (float)Math.Round(vector2.Y / blockSize) * blockSize);
        }

        public static Vector2 LerpVector2(Vector2 v1, Vector2 v2, float w)
        {
            return new Vector2(MathHelper.Lerp(v1.X, v2.X, w), MathHelper.Lerp(v1.Y, v2.Y, w));
        }

        public static IGameBlock GetBlockAbove(Vector2 originalPos, int BlockSize, List<IGameBlock> blocks)
        {
            return blocks.SingleOrDefault(b => b.GetPosition().X == originalPos.X
                        && b.GetPosition().Y == originalPos.Y - BlockSize);
        }

        public static IGameBlock GetBlockToTheRight(Vector2 originalPos, int BlockSize, List<IGameBlock> blocks)
        {
            return blocks.SingleOrDefault(b => b.GetPosition().X == originalPos.X + BlockSize
            && b.GetPosition().Y == originalPos.Y);
        }

        public static IGameBlock GetBlockToTheLeft(Vector2 originalPos, int BlockSize, List<IGameBlock> blocks)
        {
            return blocks.SingleOrDefault(b => b.GetPosition().X == originalPos.X - BlockSize
            && b.GetPosition().Y == originalPos.Y);
        }

        public static IGameBlock GetBlockBelow(Vector2 originalPos, int BlockSize, List<IGameBlock> blocks)
        {
            return blocks.SingleOrDefault(b => b.GetPosition().X == originalPos.X
            && b.GetPosition().Y == originalPos.Y + BlockSize);
        }

        public static Vector2 GetRoundedVector2(Vector2 vector2)
        {
            return new Vector2((float)Math.Round(vector2.X), (float)Math.Round(vector2.Y));
        }
    }
}