using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TowerShooter.Blocks
{
    internal class WoodBlock : IGameBlock
    {
        Vector2 Position;
        readonly Texture2D _texture2D;
        Rectangle Rectangle;
        private readonly ITowerShooter _towerShooter;
        readonly Texture2D texture2DBlockHealth80;
        readonly Texture2D texture2DBlockHealth60;
        readonly Texture2D texture2DBlockHealth40;
        readonly Texture2D texture2DBlockHealth20;

        public BlockType BlockType { get; set; } = BlockType.Wood;

        public bool ItemCanSpawnOnTop { get; set; } = true;
        public bool IsVisible { get; set; } = true;
        public bool IsSolid { get; set; } = true;
        public bool LosCollision { get; set; }
        public bool TouchesLight { get; set; }
        public bool LightCollision { get; set; }
        public Guid Guid { get; set; }

        public int Health { get; set; } = 100;

        public WoodBlock(int xPos, int yPos, ITowerShooter towerShooter)
        {
            Guid = Guid.NewGuid();

            _towerShooter = towerShooter;

            Position.X = xPos;
            Position.Y = yPos;

            _texture2D = towerShooter.ContentManager.Load<Texture2D>("WoodBlock");
            texture2DBlockHealth80 = towerShooter.ContentManager.Load<Texture2D>("BlockHealth80");
            texture2DBlockHealth60 = towerShooter.ContentManager.Load<Texture2D>("BlockHealth60");
            texture2DBlockHealth40 = towerShooter.ContentManager.Load<Texture2D>("BlockHealth40");
            texture2DBlockHealth20 = towerShooter.ContentManager.Load<Texture2D>("BlockHealth20");
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

            if (Health <= 80 && Health > 60)
                spriteBatch.Draw(texture2DBlockHealth80, Position, Color.White);
            if (Health <= 60 && Health > 40)
                spriteBatch.Draw(texture2DBlockHealth60, Position, Color.White);
            if (Health <= 40 && Health > 20)
                spriteBatch.Draw(texture2DBlockHealth40, Position, Color.White);
            if (Health <= 20 && Health > 0)
                spriteBatch.Draw(texture2DBlockHealth20, Position, Color.White);

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
            Health -= 2;
        }
    }
}