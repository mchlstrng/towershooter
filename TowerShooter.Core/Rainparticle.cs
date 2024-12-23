using Microsoft.Xna.Framework;
using System;

namespace TowerShooter
{
    public class RainParticle
    {
        private readonly float _gravity = 0.01f;

        public Vector2 Position;
        public Vector2 Velocity;
        public Rectangle Rectangle;
        private readonly ITexture2D texture;
        public bool CollidesWithBlocks = true;
        private readonly ITowerShooter towerShooter;

        public RainParticle(ITowerShooter towerShooter, Vector2 position)
        {
            this.towerShooter = towerShooter;
            texture = towerShooter.ContentManager.LoadTexture2D("rain");
            Position = position;
        }

        public void Draw(ISpriteBatch spriteBatch, GameTime gameTime)
        {
            //https://gamedev.stackexchange.com/questions/40011/how-do-i-rotate-a-sprite-so-that-it-is-pointing-in-the-direction-it-is-moving

            float angleInRadians = (float)Math.Atan2(Velocity.Y, Velocity.X);
            Vector2 spriteOrigin = new(texture.Width / 2, texture.Height / 2);

            spriteBatch.Draw(texture.GetTexture2D(), Position, sourceRectangle: null, Color.White, angleInRadians, spriteOrigin, scale: 1, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, layerDepth: 1);
        }

        public void Update(GameTime gameTime)
        {
            MoveY(gameTime);
            MoveX(gameTime);
            UpdateRectangles();
        }

        private void MoveX(GameTime gameTime)
        {
            //this should change depending on the wind speed
            Velocity.X += towerShooter.RainEngine.WindSpeed;
            Position.X += (float)(Velocity.X * gameTime.ElapsedGameTime.TotalMilliseconds);
            Position.X = (float)Math.Round(Position.X, 0);
        }

        private void UpdateRectangles()
        {
            Rectangle = new Rectangle
            {
                X = (int)Position.X,
                Y = (int)Position.Y,
                Width = texture.Width,
                Height = texture.Height
            };
        }

        private void MoveY(GameTime gameTime)
        {
            Velocity.Y += _gravity;
            Position.Y += (float)(Velocity.Y * gameTime.ElapsedGameTime.TotalMilliseconds);
            Position.Y = (float)Math.Round(Position.Y, 0);
        }
    }
}