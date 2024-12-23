using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using TowerShooter.Blocks;
using TowerShooter.Screens.Interfaces;

namespace TowerShooter.Screens
{
    public class GameplayGameScreen : IGameScreen, IGameplayGameScreen
    {
        internal ITowerShooter _towerShooter;

        private PauseMenu _pauseMenu;

        public const int BlockSize = 20;
        private int _blocksX = 0;
        private int _blocksY = 0;

        public bool GameRunning = true;

        public Microsoft.Xna.Framework.Color DrawColor { get; set; } = Microsoft.Xna.Framework.Color.White;
        public List<IGameBlock> Blocks { get; set; } = new List<IGameBlock>();

        Microsoft.Xna.Framework.Color ambientColor = Microsoft.Xna.Framework.Color.White;

        private SpriteFont _clockFont;

        private ITexture2D _pixel;

        private Sun sun;
        private Moon moon;

        public GameplayGameScreen(ITowerShooter towerShooter)
        {
            _towerShooter = towerShooter;
        }

        private void GenerateWorld()
        {
            //Generate world


            //generate world from png
            Bitmap img = new($"{AppDomain.CurrentDomain.BaseDirectory}/Levels/Level_1.png");

            _towerShooter.MapWidth = img.Width * BlockSize;
            _towerShooter.MapHeight = img.Height * BlockSize;

            _blocksX = _towerShooter.MapWidth / BlockSize;
            _blocksY = _towerShooter.MapHeight / BlockSize;


            for (int x = 0; x < _blocksX; x++)
            {
                for (int y = 0; y < _blocksY; y++)
                {
                    System.Drawing.Color pixel = img.GetPixel(x, y);
                    string pixelColorName = pixel.Name;

                    if (pixelColorName == "ff000000") //black (stone)
                    {
                        Blocks.Add(new StoneBlock(BlockSize * x, BlockSize * y, _towerShooter));
                    }
                    else if (pixelColorName == "ffffffff") //white (air)
                    {
                        Blocks.Add(new AirBlock(BlockSize * x, BlockSize * y, _towerShooter));
                    }
                    else if (pixelColorName == "ff7f3300") //brown (ladder)
                    {
                        Blocks.Add(new LadderBlock(BlockSize * x, BlockSize * y, _towerShooter));
                    }
                }
            }
        }

        public void Draw(ISpriteBatch spriteBatch, GameTime gameTime)
        {
            if (GameRunning)
            {
                DrawGameLogic(spriteBatch, gameTime);
            }
            else
            {
                _towerShooter.Camera2D.ResetToNormal();
                _towerShooter.FlashlightComponent.SetAmbientColor(Microsoft.Xna.Framework.Color.White);
                _pauseMenu.Draw(spriteBatch, gameTime);
            }

            if (_towerShooter.Player.Hp <= 0)
            {
                //player is dead. show gameover menu
                _towerShooter.ScreenManager.GetNewScreenInstance(ScreenType.GameoverMenu);
            }

        }

        private void DrawGameLogic(ISpriteBatch spriteBatch, GameTime gameTime)
        {
            Microsoft.Xna.Framework.Color aColor = new(ambientColor.R / 255f * _towerShooter.GameClock.Ambient, ambientColor.G / 255f * _towerShooter.GameClock.Ambient, ambientColor.B / 255f * _towerShooter.GameClock.Ambient);

            _towerShooter.FlashlightComponent.SetAmbientColor(aColor);

            foreach (IGameBlock block in Blocks)
            {
                if (block.IsVisible)
                    block.Draw(spriteBatch, gameTime);

                if (!block.IsVisible && _towerShooter.Debug)
                    block.Draw(spriteBatch, gameTime);
            }

            _towerShooter.Player.Draw(spriteBatch, gameTime);

            sun.Draw(spriteBatch, gameTime);
            moon.Draw(spriteBatch, gameTime);

            if (_towerShooter.Debug)
            {
                Microsoft.Xna.Framework.Rectangle mouseRect = _towerShooter.Mouse.GetMouseRectangle();
                DrawLine(spriteBatch, _towerShooter.Player.GetPosition(), new Vector2(mouseRect.X, mouseRect.Y), Microsoft.Xna.Framework.Color.White, 1);
            }

            string gameTimeAsString = _towerShooter.GameClock.GetGameClockSecondsToTimeAsString();

            spriteBatch.Begin();
            spriteBatch.DrawString(_clockFont, gameTimeAsString, new Vector2(700, 10), Microsoft.Xna.Framework.Color.White);
            spriteBatch.End();

            spriteBatch.Begin();
            spriteBatch.DrawString(_clockFont, "Day: " + _towerShooter.GameClock.CurrentDay.ToString(), new Vector2(700, 30), Microsoft.Xna.Framework.Color.White);
            spriteBatch.End();

            _towerShooter.RainEngine.Draw(spriteBatch, gameTime);
        }

        private void DrawLine(ISpriteBatch spriteBatch, Vector2 begin, Vector2 end, Microsoft.Xna.Framework.Color color, int width)
        {
            Microsoft.Xna.Framework.Rectangle r = new((int)begin.X, (int)begin.Y, (int)(end - begin).Length() + width, width);
            Vector2 v = Vector2.Normalize(begin - end);
            float angle = (float)Math.Acos(Vector2.Dot(v, -Vector2.UnitX));
            if (begin.Y > end.Y) angle = MathHelper.TwoPi - angle;
            spriteBatch.BeginCameraZoom();
            spriteBatch.Draw(_pixel.GetTexture2D(), r, null, color, angle, Vector2.Zero, SpriteEffects.None, 0);
            spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            if (GameRunning)
            {
                RunGameLogic(gameTime);

                if (_towerShooter.InputManager.IsSingleKeyPress(Keys.Escape))
                {
                    GameRunning = !GameRunning;
                }

                //camera should always follow player
                _towerShooter.Camera2D.Position = new Vector2(_towerShooter.Player.GetPosition().X, _towerShooter.Player.GetPosition().Y);

                int centerScreenX = _towerShooter.GraphicsDeviceManager.PreferredBackBufferWidth / 2;
                float mapSizeX = _towerShooter.MapWidth - (centerScreenX / _towerShooter.Camera2D.Zoom);

                int centerScreenY = _towerShooter.GraphicsDeviceManager.PreferredBackBufferHeight / 2;
                float mapSizeY = _towerShooter.MapHeight - (centerScreenY / _towerShooter.Camera2D.Zoom);

                //clamp cam
                float camXpos = _towerShooter.Camera2D.Position.X;
                camXpos = MathHelper.Clamp(camXpos, centerScreenX / _towerShooter.Camera2D.Zoom, mapSizeX);

                float camYpos = _towerShooter.Camera2D.Position.Y;
                camYpos = MathHelper.Clamp(camYpos, centerScreenY / _towerShooter.Camera2D.Zoom, mapSizeY);

                _towerShooter.Camera2D.Position = new Vector2(camXpos, camYpos);
            }
            else
            {
                _pauseMenu.Update(gameTime);
            }
        }

        private void RunGameLogic(GameTime gameTime)
        {
            _towerShooter.GameClock.Update(gameTime);

            _towerShooter.RainEngine.Update(gameTime);

            sun.Update(gameTime);
            moon.Update(gameTime);

            //day night cycle

            if (_towerShooter.Debug)
            {
                if (_towerShooter.InputManager.IsKeyDown(Keys.L))
                {
                    _towerShooter.GameClock.GameClockSeconds += 100;
                    _towerShooter.GameClock.MoonClockSeconds += 100;
                }

                if (_towerShooter.InputManager.IsKeyDown(Keys.LeftShift) && _towerShooter.InputManager.IsKeyDown(Keys.L))
                {
                    _towerShooter.GameClock.GameClockSeconds += 900;
                    _towerShooter.GameClock.MoonClockSeconds += 900;
                }
            }

            foreach (IGameBlock block in Blocks)
            {
                block.Update(gameTime);

                block.LosCollision = false;

                if (_towerShooter.Mouse.GetMouseRectangle().Intersects(block.GetRectangle()))
                {

                    _towerShooter.Player.UpdateMouseoverBlock(block);
                }

                RemoveRainParticleOnCollisionWithBlockOrOutsideBottomOfScreen(block);

            }
            _towerShooter.Player.Update(gameTime);

        }



        private void RemoveRainParticleOnCollisionWithBlockOrOutsideBottomOfScreen(IGameBlock block)
        {
            for (int i = 0; i < _towerShooter.RainEngine.RainParticles.Count; i++)
            {
                RainParticle rainparticle = _towerShooter.RainEngine.RainParticles[i];

                if (block.IsSolid && rainparticle.Rectangle.Intersects(block.GetRectangle()) && rainparticle.CollidesWithBlocks)
                {
                    _towerShooter.RainEngine.RainParticles.Remove(rainparticle);
                }
                else if (rainparticle.Position.Y > _towerShooter.ResolutionY)
                {
                    _towerShooter.RainEngine.RainParticles.Remove(rainparticle);
                }
            }
        }


        public void LoadContent()
        {
            _towerShooter.FlashlightComponent.Enable();
            _clockFont = _towerShooter.ContentManager.Load<SpriteFont>("clockFont");
            _pixel = _towerShooter.ContentManager.LoadTexture2D("pixel");

            _pauseMenu = new PauseMenu(this);
            _pauseMenu.LoadContent();

            _towerShooter.Player.LoadContent();

            GenerateWorld();

            sun = new Sun(_towerShooter);
            moon = new Moon(_towerShooter);

            _towerShooter.Player.Hp = 100;
        }
    }
}