using Microsoft.Xna.Framework.Input;

namespace TowerShooter
{
    public class GamepadImpl : IGamepad
    {
        private GamePadState _currentGamepadState;
        private GamePadState _previousGamepadState;
        private readonly TowerShooter towerShooter;

        public GamepadImpl(TowerShooter towerShooter)
        {
            this.towerShooter = towerShooter;
        }

        public bool IsButtonSinglePressA()
        {
            return _currentGamepadState.Buttons.A == ButtonState.Pressed && _previousGamepadState.Buttons.A == ButtonState.Released;
        }

        public bool IsButtonPressA()
        {
            return _currentGamepadState.Buttons.A == ButtonState.Pressed && _previousGamepadState.Buttons.A == ButtonState.Pressed;
        }

        public bool IsDpadPressLeft()
        {
            return _currentGamepadState.DPad.Left == ButtonState.Pressed && _currentGamepadState.DPad.Left == ButtonState.Pressed;
        }

        public bool IsDpadPressRight()
        {
            return _currentGamepadState.DPad.Right == ButtonState.Pressed && _currentGamepadState.DPad.Right == ButtonState.Pressed;
        }

        public bool IsDpadPressDown()
        {
            return _currentGamepadState.DPad.Down == ButtonState.Pressed && _currentGamepadState.DPad.Down == ButtonState.Pressed;
        }

        public void Update()
        {
            _previousGamepadState = _currentGamepadState;
            _currentGamepadState = towerShooter.GamepadState.GetState();
        }

    }

}