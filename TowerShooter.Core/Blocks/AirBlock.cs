using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TowerShooter.Blocks
{
    internal class AirBlock : IGameBlock
    {
        Vector2 Position;
        readonly Texture2D _texture2D;
        Rectangle Rectangle;
        private readonly ITowerShooter _towerShooter;

        public BlockType BlockType { get; set; } = BlockType.Air;

        public bool TouchesLight { get; set; }

        public bool IsVisible { get; set; } = true;
        public bool IsSolid { get; set; } = false;

        public Guid Guid { get; set; }
        public bool LosCollision { get; set; }
        public bool LightCollision { get; set; }
        public int Health { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool ItemCanSpawnOnTop { get; set; }
        public AirBlock(int xPos, int yPos, ITowerShooter towerShooter)
        {
            _towerShooter = towerShooter;
            Guid = Guid.NewGuid();

            Position.X = xPos;
            Position.Y = yPos;

            _texture2D = towerShooter.ContentManager.Load<Texture2D>("AirBlock");
            UpdateRectangles();
        }

        public void Draw(ISpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.BeginCameraZoom();

            Color color;

            if (_towerShooter.Debug && LosCollision)
                color = Color.Red;
            else
                color = Color.White;

            spriteBatch.Draw(_texture2D, Position, color);

            spriteBatch.End();
        }

        public Vector2 GetSize()
        {
            return new Vector2(_texture2D.Width, _texture2D.Height);
        }

        public void Update(GameTime gameTime)
        {
            UpdateRectangles();
        }

        private void UpdateRectangles()
        {
            Rectangle = new Rectangle();
            Rectangle.X = (int)Position.X;
            Rectangle.Y = (int)Position.Y;
            Rectangle.Width = _texture2D.Width;
            Rectangle.Height = _texture2D.Height;
        }

        public Rectangle GetRectangle()
        {
            return Rectangle;
        }

        public Vector2 GetPosition()
        {
            return Position;
        }

        public void Mine()
        {
            throw new NotImplementedException();
        }
    }
}