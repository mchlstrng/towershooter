using Microsoft.Xna.Framework.Input;

namespace TowerShooter
{
    public class KeyboardStateImpl : IKeyboardStateImpl
    {
        public KeyboardState GetState()
        {
            return Keyboard.GetState();
        }
    }
}