using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TowerShooter.Blocks
{
    internal class HealthPackBlock : IGameBlock
    {
        Vector2 Position;
        readonly Texture2D _texture2D;
        Rectangle Rectangle;
        private readonly ITowerShooter _towerShooter;

        public bool IsVisible { get; set; } = true;
        public bool IsSolid { get; set; } = false;
        public bool LosCollision { get; set; }
        public bool LightCollision { get; set; }
        public bool ItemCanSpawnOnTop { get; set; } = true;
        public BlockType BlockType { get; set; } = BlockType.HealthPack;

        public bool TouchesLight { get; set; }

        public Guid Guid { get; set; }
        public int Health { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public HealthPackBlock(int xPos, int yPos, ITowerShooter towerShooter)
        {
            _towerShooter = towerShooter;

            Guid = Guid.NewGuid();

            Position.X = xPos;
            Position.Y = yPos;

            _texture2D = _towerShooter.ContentManager.Load<Texture2D>("Health");
            UpdateRectangle();
        }

        public void Draw(ISpriteBatch spriteBatch, GameTime gameTime)
        {
            Color color = Color.White;

            if (_towerShooter.Debug && LosCollision)
                color = Color.Red;
            else
                color = Color.White;

            spriteBatch.BeginCameraZoom();
            spriteBatch.Draw(_texture2D, Position, color);
            spriteBatch.End();
        }

        public Vector2 GetSize()
        {
            return new Vector2(_texture2D.Width, _texture2D.Height);
        }

        public void Update(GameTime gameTime)
        {
            UpdateRectangle();
        }

        private void UpdateRectangle()
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