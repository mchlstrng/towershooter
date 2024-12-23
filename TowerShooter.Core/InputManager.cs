using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TowerShooter
{
    public class InputManager : IInputManager
    {
        private readonly ITowerShooter _towerShooter;

        public InputManager(ITowerShooter towerShooter)
        {
            _towerShooter = towerShooter;
        }

        public void Update(GameTime gameTime)
        {
            _towerShooter.Keyboard.Update();
            _towerShooter.Mouse.Update();
            _towerShooter.Gamepad.Update();
        }

        public bool IsLeftClickDown()
        {
            return _towerShooter.Mouse.IsLeftClickDown();
        }

        public bool IsRightClickDown()
        {
            return _towerShooter.Mouse.IsRightClickDown();
        }

        public bool IsSingleLeftClick()
        {
            return _towerShooter.Mouse.IsSingleLeftClick();
        }

        public bool IsSingleRightClick()
        {
            return _towerShooter.Mouse.IsSingleRightClick();
        }

        public bool IsKeyDown(Keys key)
        {
            return _towerShooter.Keyboard.IsKeyDown(key);
        }

        public bool IsSingleKeyPress(Keys key)
        {
            return _towerShooter.Keyboard.IsSingleKeyPress(key);
        }

        public bool IsScrollUp()
        {
            return _towerShooter.Mouse.IsScrollUp();
        }

        public bool IsScrollDown()
        {
            return _towerShooter.Mouse.IsScrollDown();
        }
    }
}