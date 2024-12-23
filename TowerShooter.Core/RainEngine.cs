using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace TowerShooter
{
    public class RainEngine : IRainEngine
    {
        public List<RainParticle> RainParticles { get; set; } = new List<RainParticle>();
        private readonly ITowerShooter towerShooter;

        public bool Enabled { get; set; } = true;
        public float WindSpeed { get; set; } = 0f;

        public RainEngine(ITowerShooter towerShooter)
        {
            this.towerShooter = towerShooter;
        }

        public void Update(GameTime gameTime)
        {
            if (RainParticles.Count < 100 && Enabled)
            {
                //spawn rainparticle at random position
                int xPos = new Random().Next(0, towerShooter.ResolutionX - 1);
                int yPos = new Random().Next(-50, 0);
                RainParticles.Add(new RainParticle(towerShooter, new Vector2(xPos, yPos)));
            }


            if (towerShooter.Debug)
            {
                if (towerShooter.InputManager.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.U))
                    WindSpeed += 0.001f;
                if (towerShooter.InputManager.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.I))
                    WindSpeed -= 0.001f;
            }

            WindSpeed = MathHelper.Clamp(WindSpeed, -0.025f, 0.025f);

            foreach (RainParticle rainParticle in RainParticles)
            {
                rainParticle.Update(gameTime);
            }
        }

        public void Draw(ISpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.BeginCameraZoom();

            foreach (RainParticle rainParticle in RainParticles)
            {
                rainParticle.Draw(spriteBatch, gameTime);
            }
            spriteBatch.End();
        }
    }
}