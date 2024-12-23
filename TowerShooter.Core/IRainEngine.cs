using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace TowerShooter
{
    public interface IRainEngine
    {
        void Draw(ISpriteBatch spriteBatch, GameTime gameTime);
        void Update(GameTime gameTime);
        public List<RainParticle> RainParticles { get; set; }
        bool Enabled { get; set; }
        float WindSpeed { get; set; }
    }
}