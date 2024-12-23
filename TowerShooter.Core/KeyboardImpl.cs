using Microsoft.Xna.Framework.Input;

namespace TowerShooter
{

    public class KeyboardImpl : IKeyboard
    {
        private KeyboardState _currentKeyboardState;
        private KeyboardState _previousKeyboardState;
        private readonly ITowerShooter towerShooter;

        public KeyboardImpl(ITowerShooter towerShooter)
        {
            this.towerShooter = towerShooter;
        }

        public void Update()
        {
            _previousKeyboardState = _currentKeyboardState;
            _currentKeyboardState = towerShooter.KeyboardState.GetState();
        }

        public bool IsSingleKeyPress(Keys key)
        {
            return _currentKeyboardState.IsKeyDown(key) && _previousKeyboardState.IsKeyUp(key);
        }

        public bool IsKeyDown(Keys key)
        {
            return _currentKeyboardState.IsKeyDown(key) && _previousKeyboardState.IsKeyDown(key);
        }
    }
}