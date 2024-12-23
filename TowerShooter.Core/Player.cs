using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Penumbra;
using System;
using System.Collections.Generic;
using System.Linq;
using TowerShooter.Blocks;
using TowerShooter.Screens;

namespace TowerShooter
{
    public class Player : IPlayer
    {
        /// <summary>
        /// The force of player jump height
        /// </summary>
        private const float _jumpHForce = -0.15f;

        internal MouseOverBlock _mouseOverBlock;

        /// <summary>
        /// Player gravity
        /// </summary>
        private readonly float _gravity = 0.01f;

        /// <summary>
        /// Reference to the gameplay game screen
        /// </summary>
        public IGameplayGameScreen _gameplayGameScreen;

        /// <summary>
        /// Texture to apply when standing
        /// </summary>
        private ITexture2D _textureStanding;

        /// <summary>
        /// Texture to apply when crouching
        /// </summary>
        private ITexture2D _textureCrouching;

        /// <summary>
        /// Single pixel texture
        /// </summary>
        private ITexture2D _pixel;

        /// <summary>
        /// Pickaxe texture
        /// </summary>
        private ITexture2D _pickaxe;

        private Vector2 _position;

        /// <summary>
        /// Players velocity
        /// </summary>
        public Vector2 Velocity;

        /// <summary>
        /// Collision rect for the player
        /// </summary>
        public Rectangle Rectangle;

        /// <summary>
        /// Players ground rectangle
        /// </summary>
        public Rectangle GroundRectangle;

        private CollisionLineResult collisionLineResult;

        /// <summary>
        /// Players top rectangle
        /// </summary>
        public Rectangle TopRectangle;

        /// <summary>
        /// Are we standing on a block?
        /// </summary>
        private bool _standingOnBlock = false;

        /// <summary>
        /// Is there a block above us?
        /// </summary>
        private bool _blockAbove = false;

        /// <summary>
        /// Players movement speed
        /// </summary>
        private readonly float _movementSpeed = 0.05f;

        private const float MaxDiagonalDistance = 100;

        /// <summary>
        /// Players health
        /// </summary>
        public int Hp { get; set; } = 100;

        /// <summary>
        /// Font for the player hp
        /// </summary>
        private ISpriteFont _hpFont;

        /// <summary>
        /// Rect for player hp
        /// </summary>
        private Rectangle _hpRect;

        /// <summary>
        /// Texture for player go rect
        /// </summary>
        private ITexture2D _hpRectTexture;

        /// <summary>
        /// Are we crouching?
        /// </summary>
        private bool crouching = false;

        /// <summary>
        /// Do we touch a ladder?
        /// </summary>
        public bool TouchesLadder = false;

        /// <summary>
        /// How many ladders are we touching?
        /// </summary>
        private int HowManyLaddersDoWeTouch = 0;

        /// <summary>
        /// Is our flashlight turned on?
        /// </summary>
        public bool Flashlight = false;

        private IInventory inventory;

        public bool CanPlace { get; set; }
        public bool CanRemove { get; set; }

        private readonly ITowerShooter _towerShooter;

        private ISoundEffect miningSoundEffect;
        private ISoundEffect hurtSoundEffect;

        private ISoundEffect pickupSound;
        private ISoundEffect hpPickupSound;

        private readonly int playerBlockRadius = 50;
        private IEnumerable<IGameBlock> blocksAroundThePlayer;

        public Player(ITowerShooter towerShooter)
        {
            _towerShooter = towerShooter;
        }

        public Vector2 GetPosition()
        {
            return _position;
        }

        private bool DoesNotCollideWithMoreThanXSolidBlocks(int num)
        {
            int solidBlockColCount = _gameplayGameScreen.Blocks.Count(b => b.IsSolid && b.LosCollision);
            return solidBlockColCount <= num;
        }

        private void RemoveBlock()
        {
            CanRemove = true;

            if (_mouseOverBlock.BlockWeTouchWithMouse != null && !_mouseOverBlock.BlockWeTouchWithMouse.IsSolid)
                CanRemove = false;

            if (!DoesNotCollideWithMoreThanXSolidBlocks(1))
                CanRemove = false;

            if (collisionLineResult?.DiagonalDistance > MaxDiagonalDistance)
                CanRemove = false;

            if (_towerShooter.InputManager.IsRightClickDown())
            {
                if (CanRemove)
                {
                    IGameBlock block = _mouseOverBlock.BlockWeTouchWithMouse;

                    bool isCreative = _towerShooter.IsCreative;
                    if (isCreative)
                    {
                        block.Health = 0;
                    }
                    else
                    {
                        _towerShooter.SoundEffectPlayer.PlaySoundEffect(_towerShooter.GameTime, miningSoundEffect, 0.2f);
                        block.Mine();
                    }

                    if (block.Health <= 0)
                    {
                        Vector2 pos = _mouseOverBlock.GetPosition();
                        _gameplayGameScreen.Blocks.Remove(_mouseOverBlock.BlockWeTouchWithMouse);
                        _gameplayGameScreen.Blocks.Add(new AirBlock((int)pos.X, (int)pos.Y, _towerShooter));

                        pickupSound.Play();
                        inventory.AddToExisting(_mouseOverBlock.BlockWeTouchWithMouse.BlockType);
                    }
                }
            }
        }

        private IGameBlock CreateBlock(BlockType blockType, int x, int y)
        {
            if (blockType == BlockType.Stone)
            {
                return new StoneBlock(x, y, _towerShooter);
            }
            if (blockType == BlockType.Wood)
            {
                return new WoodBlock(x, y, _towerShooter);
            }
            return null;
        }

        private void PlaceBlock()
        {
            CheckIfCanPlace();

            if (_towerShooter.InputManager.IsSingleLeftClick() && CanPlace)
            {


                InventoryItem activeInventoryItem = inventory.GetActiveInventoryItem();
                if (activeInventoryItem != null && activeInventoryItem.Amount >= 1)
                {
                    Vector2 pos = _mouseOverBlock.GetPosition();
                    _gameplayGameScreen.Blocks.Remove(_mouseOverBlock.BlockWeTouchWithMouse);
                    _gameplayGameScreen.Blocks.Add(CreateBlock(activeInventoryItem.BlockType, (int)pos.X, (int)pos.Y));


                    if (!_towerShooter.IsCreative)
                    {
                        inventory.RemoveItem(activeInventoryItem.BlockType);
                    }
                }
            }
        }

        private void CheckIfCanPlace()
        {
            //place block
            //only place block if we try to place on the ground or is connected to another block
            CanPlace = false;

            //can place on ground
            if (_mouseOverBlock.GetRectangle().Y == _towerShooter.MapHeight - GameplayGameScreen.BlockSize)
                CanPlace = true;

            //can place on top of another block
            float yPosToCheck = _mouseOverBlock.GetPosition().Y + GameplayGameScreen.BlockSize;

            //check block below

            Vector2 originalPos = _mouseOverBlock.GetPosition();

            IGameBlock blockBelow = Util.GetBlockBelow(originalPos, GameplayGameScreen.BlockSize, _gameplayGameScreen.Blocks);

            if (blockBelow != null && yPosToCheck <= _towerShooter.MapHeight
                && blockBelow.IsSolid)
            {
                CanPlace = true;
            }

            //check block to the left

            IGameBlock blockToTheLeft = Util.GetBlockToTheLeft(originalPos, GameplayGameScreen.BlockSize, _gameplayGameScreen.Blocks);

            if (blockToTheLeft != null && yPosToCheck <= _towerShooter.MapHeight
                && blockToTheLeft.IsSolid)
            {
                CanPlace = true;
            }

            //check block to the right

            IGameBlock blockToTheRight = Util.GetBlockToTheRight(originalPos, GameplayGameScreen.BlockSize, _gameplayGameScreen.Blocks);

            if (blockToTheRight != null && yPosToCheck <= _towerShooter.MapHeight
                && blockToTheRight.IsSolid)
            {
                CanPlace = true;
            }

            //check block above

            IGameBlock blockAbove = Util.GetBlockAbove(originalPos, GameplayGameScreen.BlockSize, _gameplayGameScreen.Blocks);

            if (blockAbove != null && yPosToCheck <= _towerShooter.MapHeight
                && blockAbove.IsSolid)
            {
                CanPlace = true;
            }

            if (_mouseOverBlock.GetRectangle().Intersects(Rectangle))
            {
                CanPlace = false;
            }

            if (!DoesNotCollideWithSolidBlock())
            {
                CanPlace = false;
            }

            if (_mouseOverBlock.BlockWeTouchWithMouse != null && _mouseOverBlock.BlockWeTouchWithMouse.GetType() != typeof(AirBlock))
            {
                CanPlace = false;
            }

            if (collisionLineResult?.DiagonalDistance > MaxDiagonalDistance)
                CanPlace = false;

            bool isCreative = _towerShooter.IsCreative;

            if (inventory.GetActiveInventoryItem()?.Amount <= 0 && !isCreative)
                CanPlace = false;
        }

        public bool DoesNotCollideWithSolidBlock()
        {
            return !_gameplayGameScreen.Blocks.Any(b => b.IsSolid && b.LosCollision);
        }

        public void UpdateMouseoverBlock(IGameBlock block)
        {
            _mouseOverBlock.SetPosition(block.GetPosition());
            _mouseOverBlock.BlockWeTouchWithMouse = block;
        }

        public void Update(GameTime gameTime)
        {

            //get blocks in radius of x pixels from the player
            blocksAroundThePlayer = _gameplayGameScreen.Blocks.Where(b =>
            b.GetPosition().X >= _position.X - playerBlockRadius && b.GetPosition().X <= _position.X + playerBlockRadius &&
            b.GetPosition().Y >= _position.Y - playerBlockRadius && b.GetPosition().Y <= _position.Y + playerBlockRadius).ToList();

            PlayerBlockCollisionLine();

            UpdateRectangles();
            MoveX(gameTime);
            MoveY(gameTime);
            SetTouchesLadder();
            UpdateRectangles();

            PlaceBlock();

            RemoveBlock();

            if (_towerShooter.InputManager.IsSingleKeyPress(Keys.PageUp) || _towerShooter.InputManager.IsScrollUp())
                inventory.GoToPreviousInventoryItem();

            if (_towerShooter.InputManager.IsSingleKeyPress(Keys.PageDown) || _towerShooter.InputManager.IsScrollDown())
                inventory.GoToNextInventoryItem();

            _mouseOverBlock.Update(gameTime);

            //flashlight
            if (_towerShooter.InputManager.IsSingleKeyPress(Keys.F))
                Flashlight = !Flashlight;

            const string lightName = "player_flashlight";
            Spotlight light = (Spotlight)_towerShooter.FlashlightComponent.GetLight<Spotlight>(lightName);

            if (Flashlight)
            {
                if (light == null)
                {
                    light = new Spotlight();
                    _towerShooter.FlashlightComponent.AddLight(lightName, light);
                }
                light.Position = new Vector2(GetPosition().X + (_textureStanding.Width / 2), GetPosition().Y);

                Rectangle mouseRect = _towerShooter.Mouse.GetMouseRectangle();

                Vector2 mousePosition = new(mouseRect.X, mouseRect.Y);
                Vector2 dPos = mousePosition - light.Position;

                light.Rotation = (float)Math.Atan2(dPos.Y, dPos.X);
                light.Enabled = true;
            }

            if (light != null && !Flashlight)
            {
                light.Enabled = false;
            }

            HealthPackCollision();
        }

        private void HealthPackCollision()
        {
            //collision with healthpack
            IGameBlock healthPackWeCollideWith = _towerShooter.GameplayGameScreen.Blocks.SingleOrDefault(b => b.GetRectangle().Intersects(Rectangle) && b.BlockType == BlockType.HealthPack);
            if (healthPackWeCollideWith != null)
            {
                Hp += 10;
                Hp = MathHelper.Clamp(Hp, 0, 100);
                _towerShooter.GameplayGameScreen.Blocks.Remove(healthPackWeCollideWith);
                _towerShooter.GameplayGameScreen.Blocks.Add(new AirBlock((int)healthPackWeCollideWith.GetPosition().X, (int)healthPackWeCollideWith.GetPosition().Y, _towerShooter));
                _towerShooter.GameClock.SpawnHpClockSeconds = 0;
                hpPickupSound.Play();
            }
        }

        private void SetTouchesLadder()
        {
            TouchesLadder = false;
            HowManyLaddersDoWeTouch = 0;
            foreach (IGameBlock ladder in _gameplayGameScreen.Blocks.Where(l => l.GetType() == typeof(LadderBlock)))
            {
                if (Rectangle.Intersects(ladder.GetRectangle()))
                {
                    HowManyLaddersDoWeTouch++;
                }
            }

            //check if touches ladder
            foreach (IGameBlock ladder in _gameplayGameScreen.Blocks.Where(l => l.GetType() == typeof(LadderBlock)))
            {
                if (Rectangle.Intersects(ladder.GetRectangle()))
                {
                    TouchesLadder = true;

                    //if we touch the ladder and movement is down, fix so that we stand on top of the ladder
                    if (Velocity.Y > 0 && HowManyLaddersDoWeTouch == 1 && !_towerShooter.InputManager.IsKeyDown(Keys.S) && !_towerShooter.Gamepad.IsDpadPressDown())
                    {
                        CheckIfApplyFallDamage();

                        Velocity.Y = 0;
                        while (Rectangle.Intersects(ladder.GetRectangle()))
                        {
                            _position.Y -= 1f;
                            UpdateRectangles();
                        }
                    }
                }
            }
        }

        private void MoveY(GameTime gameTime)
        {
            //gravity
            Velocity.Y += _gravity;

            //jumping
            if (_towerShooter.InputManager.IsSingleKeyPress(Keys.W) || _towerShooter.Gamepad.IsButtonSinglePressA())
            {
                //do not jump if we touch a ladder
                if (!crouching && !TouchesLadder)
                {
                    if (_position.Y >= _towerShooter.MapHeight - _textureStanding.Height || _standingOnBlock)
                        Velocity.Y = _jumpHForce;
                }
            }
            //climp ladder up / down
            if (!crouching && TouchesLadder)
            {
                if (_towerShooter.InputManager.IsKeyDown(Keys.W) || _towerShooter.Gamepad.IsButtonPressA()) //climb up the ladder
                {
                    Velocity.Y = _movementSpeed * -1;
                }
                else if (_towerShooter.InputManager.IsKeyDown(Keys.S) || _towerShooter.Gamepad.IsDpadPressDown())
                //climb down the ladder
                {
                    if (!_standingOnBlock)
                        Velocity.Y = _movementSpeed;
                }
                else //stand still on ladder
                {
                    Velocity.Y = 0;
                }
            }

            if (!_blockAbove && (!_towerShooter.InputManager.IsKeyDown(Keys.S) || _towerShooter.Gamepad.IsDpadPressDown() || TouchesLadder || !_standingOnBlock))
            {
                crouching = false;
            }

            if (!crouching && _standingOnBlock && (_towerShooter.InputManager.IsKeyDown(Keys.S) || _towerShooter.Gamepad.IsDpadPressDown()) && !TouchesLadder && Velocity.Y > 0)
            {
                crouching = true;
                _position.Y += _textureCrouching.Height;
                UpdateRectangles();
            }

            _position.Y += (float)(Velocity.Y * gameTime.ElapsedGameTime.TotalMilliseconds);
            _position.Y = (float)Math.Round(_position.Y, 0);
            UpdateRectangles();

            //collision y

            int maxYPos = 0;

            if (crouching)
            {
                maxYPos = _towerShooter.MapHeight - _textureCrouching.Height;
                _position.Y = MathHelper.Clamp(_position.Y, 0, maxYPos);
            }
            else if (!_standingOnBlock)
            {
                maxYPos = _towerShooter.MapHeight - _textureStanding.Height;
                _position.Y = MathHelper.Clamp(_position.Y, 0, maxYPos);
            }

            //fall damage on lower level limit (ground)
            if (Velocity.Y > 0 && _position.Y == maxYPos)
            {
                CheckIfApplyFallDamage();
                Velocity.Y = 0;
            }

            _standingOnBlock = false;
            _blockAbove = false;
            foreach (IGameBlock block in blocksAroundThePlayer.Where(b => b.IsSolid))
            {
                //down
                if (Rectangle.Intersects(block.GetRectangle()))
                {
                    if (Velocity.Y > 0)
                    {
                        CheckIfApplyFallDamage();
                        Velocity.Y = 0;
                        while (Rectangle.Intersects(block.GetRectangle()))
                        {
                            _position.Y -= 1f;
                            UpdateRectangles();
                        }
                    }

                    //up
                    if (Velocity.Y < 0)
                    {
                        Velocity.Y = 0;
                        while (Rectangle.Intersects(block.GetRectangle()))
                        {
                            _position.Y += 1f;
                            UpdateRectangles();
                        }
                    }
                }

                if (GroundRectangle.Intersects(block.GetRectangle()))
                {
                    _standingOnBlock = true;
                }

                if (TopRectangle.Intersects(block.GetRectangle()))
                {
                    _blockAbove = true;
                }

            }

        }

        private void CheckIfApplyFallDamage()
        {
            int valueToSubtract = 0;

            //if we fall faster than the safe speed, we should subtract hp from the player (fall damage)
            if (Velocity.Y > 0.7f)
            {
                valueToSubtract = 40;
            }
            else if (Velocity.Y > 0.6f)
            {
                valueToSubtract = 30;
            }
            else if (Velocity.Y > 0.5f)
            {
                valueToSubtract = 20;
            }
            else if (Velocity.Y > 0.4f)
            {
                valueToSubtract = 10;
            }

            if (valueToSubtract > 0)
            {
                Hp -= valueToSubtract;
                hurtSoundEffect.Play();
            }
        }

        private void MoveX(GameTime gameTime)
        {
            if (_towerShooter.InputManager.IsKeyDown(Keys.A) || _towerShooter.Gamepad.IsDpadPressLeft())
            {
                Velocity.X = -_movementSpeed;
            }
            else if (_towerShooter.InputManager.IsKeyDown(Keys.D) || _towerShooter.Gamepad.IsDpadPressRight())
            {
                Velocity.X = _movementSpeed;
            }
            else
            {
                Velocity.X = 0;
            }

            //move x
            _position.X += (float)(Velocity.X * gameTime.ElapsedGameTime.TotalMilliseconds);
            //round x pos to nearest whole number int
            _position.X = (float)Math.Round(_position.X, 0);
            UpdateRectangles();

            //collision x
            _position.X = MathHelper.Clamp(_position.X, 0, _towerShooter.MapWidth - _textureStanding.Width);

            foreach (IGameBlock block in blocksAroundThePlayer.Where(b => b.IsSolid))
            {
                //right

                if (Rectangle.Intersects(block.GetRectangle()))
                {
                    if (Velocity.X > 0)
                    {
                        while (Rectangle.Intersects(block.GetRectangle()))
                        {
                            _position.X -= 1f;
                            Velocity.X = 0;
                            UpdateRectangles();
                        }
                    }

                    if (Velocity.X < 0)
                    {
                        while (Rectangle.Intersects(block.GetRectangle()))
                        {
                            _position.X += 1f;
                            Velocity.X = 0;
                            UpdateRectangles();
                        }
                    }
                }

            }
            UpdateRectangles();
        }

        public void Draw(ISpriteBatch spriteBatch, GameTime gameTime)
        {

            _mouseOverBlock.Draw(spriteBatch, gameTime);

            spriteBatch.BeginCameraZoom();
            Color color = Color.White;
            if (Flashlight)
                color = Color.White;

            //draw hp rect
            spriteBatch.Draw(_hpRectTexture.GetTexture2D(), _hpRect, color);
            //draw player
            if (!crouching)
            {
                spriteBatch.Draw(_textureStanding.GetTexture2D(), _position, color);
            }
            else
            {
                spriteBatch.Draw(_textureCrouching.GetTexture2D(), _position, color);
            }

            //draw inventory
            if (inventory.InventoryVisible)
            {
                inventory.Draw(spriteBatch, gameTime);
            }

            if (_towerShooter.Debug)
            {
                spriteBatch.Draw(_pixel.GetTexture2D(), TopRectangle, Color.Blue);
                spriteBatch.Draw(_pixel.GetTexture2D(), GroundRectangle, Color.Blue);
                spriteBatch.Draw(_pixel.GetTexture2D(), Rectangle, Color.Red);
            }
            spriteBatch.End();

            //draw hp
            spriteBatch.Begin();
            spriteBatch.DrawString(_hpFont.GetSpriteFont(), $"HP: {Hp}", new Vector2(10, 10), Color.White);
            DrawActiveInventoryItem(spriteBatch);

            spriteBatch.End();
        }

        private void DrawActiveInventoryItem(ISpriteBatch spriteBatch)
        {
            bool isCreative = _towerShooter.IsCreative;

            InventoryItem activeInventoryItem = inventory.GetActiveInventoryItem();
            if (activeInventoryItem != null)
            {
                //draw black border around the texture
                spriteBatch.Draw(_pixel.GetTexture2D(), new Rectangle(9, 36, activeInventoryItem.Texture.GetTexture2D().Width + 2, activeInventoryItem.Texture.GetTexture2D().Height + 2), Color.Black);
                spriteBatch.Draw(activeInventoryItem.Texture.GetTexture2D(), new Vector2(10, 37), Color.White);
                if (isCreative)
                {
                    spriteBatch.DrawString(_hpFont.GetSpriteFont(), $"Active item: {activeInventoryItem.BlockType}", new Vector2(35, 35), Color.White);
                }
                else
                {
                    spriteBatch.DrawString(_hpFont.GetSpriteFont(), $"Active item: {activeInventoryItem.BlockType} ({activeInventoryItem.Amount})", new Vector2(35, 35), Color.White);
                }
            }
        }

        private void UpdateRectangles()
        {
            Rectangle = new Rectangle();
            Rectangle.X = (int)_position.X;
            Rectangle.Y = (int)_position.Y;
            Rectangle.Width = _textureStanding.Width;

            if (crouching)
                Rectangle.Height = _textureCrouching.Height;
            else
                Rectangle.Height = _textureStanding.Height;

            GroundRectangle = new Rectangle();
            GroundRectangle.X = (int)_position.X + 1;

            if (crouching)
                GroundRectangle.Y = (int)_position.Y + _textureCrouching.Height;
            else
                GroundRectangle.Y = (int)_position.Y + _textureStanding.Height;

            GroundRectangle.Width = _textureStanding.Width - 2;
            GroundRectangle.Height = 2;

            TopRectangle = new Rectangle();
            TopRectangle.X = (int)_position.X;
            TopRectangle.Y = (int)_position.Y - 5;
            TopRectangle.Width = _textureStanding.Width;
            TopRectangle.Height = 5;

            _hpRect = new Rectangle();
            _hpRect.X = (int)_position.X - 1;
            //draw hp bar above player
            _hpRect.Y = (int)_position.Y - 10;
            _hpRect.Width = Hp / 5;
            _hpRect.Height = 5;
        }

        private IGameBlock GetBlockFromCoords(Vector2 p)
        {
            Vector2 pos = Util.RoundVector2ForBlocks(p, GameplayGameScreen.BlockSize);
            IGameBlock blockWithPos = _gameplayGameScreen.Blocks.SingleOrDefault(b => b.GetPosition().X == pos.X
                && b.GetPosition().Y == pos.Y);
            return blockWithPos;
        }

        private void PlayerBlockCollisionLine()
        {
            IGameBlock blockThatTouchesPlayer = GetBlockFromCoords(_position);
            IGameBlock mouseoverBlockTouchesBlock = _mouseOverBlock.BlockWeTouchWithMouse;

            if (blockThatTouchesPlayer != null && mouseoverBlockTouchesBlock != null)
            {
                collisionLineResult = Util.GetBlocksInCollisionLine(mouseoverBlockTouchesBlock.GetPosition(), blockThatTouchesPlayer.GetPosition(), GameplayGameScreen.BlockSize, _gameplayGameScreen.Blocks, false);
                foreach (IGameBlock b in collisionLineResult.Blocks)
                {
                    b.LosCollision = true;
                }
            }
        }

        public void LoadContent()
        {
            _gameplayGameScreen = _towerShooter.GameplayGameScreen;
            _textureStanding = _towerShooter.ContentManager.LoadTexture2D("Player");
            _textureCrouching = _towerShooter.ContentManager.LoadTexture2D("playerCrouch");
            _hpRectTexture = _towerShooter.ContentManager.LoadTexture2D("hpRectTexture");
            _pixel = _towerShooter.ContentManager.LoadTexture2D("pixel");
            _hpFont = _towerShooter.ContentManager.LoadSpriteFont("hpFont");
            miningSoundEffect = _towerShooter.ContentManager.LoadSoundEffect("mining");
            hurtSoundEffect = _towerShooter.ContentManager.LoadSoundEffect("hurt");
            _position = new Vector2(100, 500);
            pickupSound = _towerShooter.ContentManager.LoadSoundEffect("pickup");
            hpPickupSound = _towerShooter.ContentManager.LoadSoundEffect("hppickup");
            inventory = _towerShooter.Inventory;
            _pickaxe = _towerShooter.ContentManager.LoadTexture2D("pickaxe");

            _mouseOverBlock = new MouseOverBlock(-1000, -1000, _towerShooter);

            //setup inventory
            inventory.Add(new InventoryItem
            {
                Amount = 5,
                BlockType = BlockType.Stone
            });

            inventory.Add(new InventoryItem
            {
                Amount = 5,
                BlockType = BlockType.Wood
            });
        }
    }
}