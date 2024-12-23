using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TowerShooter
{
    public class MouseImpl : IMouse
    {
        private MouseState _currentMouseState;
        private MouseState _previousMouseState;

        private Rectangle _mouseRectangle;
        private readonly ITowerShooter towerShooter;

        private int _previousScrollValue = 0;
        private int _currentScrollValue = 0;

        public MouseImpl(ITowerShooter towerShooter)
        {
            this.towerShooter = towerShooter;
        }

        public void Update()
        {
            _previousMouseState = _currentMouseState;
            _currentMouseState = towerShooter.MouseState.GetState();

            _previousScrollValue = _currentScrollValue;
            _currentScrollValue = _currentMouseState.ScrollWheelValue;
            UpdateMouseRectangle();
        }

        private void UpdateMouseRectangle()
        {
            _mouseRectangle = new Rectangle();
            _mouseRectangle.Height = 1;
            _mouseRectangle.Width = 1;

            //we need to add the camera's position to the mouse position 
            //to convert the location from screen coordinates to world coordinates

            Vector2 mouseWorldPosition = Vector2.Transform(new Vector2(_currentMouseState.X, _currentMouseState.Y), Matrix.Invert(towerShooter.Camera2D.ViewMatrix));

            _mouseRectangle.X = (int)mouseWorldPosition.X;
            _mouseRectangle.Y = (int)mouseWorldPosition.Y;
        }

        public Rectangle GetMouseRectangle()
        {
            return _mouseRectangle;
        }

        public bool IsSingleLeftClick()
        {
            return _currentMouseState.LeftButton == ButtonState.Pressed && _previousMouseState.LeftButton == ButtonState.Released;
        }

        public bool IsLeftClickDown()
        {
            return _currentMouseState.LeftButton == ButtonState.Pressed && _previousMouseState.LeftButton == ButtonState.Pressed;
        }

        public bool IsSingleRightClick()
        {
            return _currentMouseState.RightButton == ButtonState.Pressed && _previousMouseState.RightButton == ButtonState.Released;
        }

        public bool IsRightClickDown()
        {
            return _currentMouseState.RightButton == ButtonState.Pressed && _previousMouseState.RightButton == ButtonState.Pressed;
        }

        public bool IsScrollUp()
        {
            return _previousScrollValue < _currentScrollValue;
        }

        public bool IsScrollDown()
        {
            return _previousScrollValue > _currentScrollValue;
        }
    }
}