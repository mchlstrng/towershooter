using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TowerShooter.Blocks
{
    internal class MouseOverBlock : IGameBlock
    {
        Vector2 Position;
        readonly Texture2D _texture2D;
        Rectangle Rectangle;
        private readonly ITowerShooter _towerShooter;

        public bool IsVisible { get; set; } = false;
        public bool IsSolid { get; set; } = false;

        public bool LightCollision { get; set; }
        public Guid Guid { get; set; }

        public BlockType BlockType { get; set; } = BlockType.MouseOver;

        public IGameBlock BlockWeTouchWithMouse { get; set; }
        bool IGameBlock.LosCollision { get; set; }

        public bool TouchesLight { get; set; }
        public int Health { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool ItemCanSpawnOnTop { get; set; }

        public MouseOverBlock(int xPos, int yPos, ITowerShooter towerShooter)
        {
            _towerShooter = towerShooter;

            Guid = Guid.NewGuid();

            Position.X = xPos;
            Position.Y = yPos;

            _texture2D = _towerShooter.ContentManager.Load<Texture2D>("MouseOverBlock");
            UpdateRectangle();
        }

        public void Draw(ISpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.BeginCameraZoom();

            IPlayer player = _towerShooter.Player;

            Color color;
            if (player.CanPlace)
                color = Color.Green;
            else if (player.CanRemove)
                color = Color.Blue;
            else
                color = Color.Red;

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

        internal void SetPosition(Vector2 position)
        {
            Position = position;
        }

        public void Mine()
        {

        }
    }
}