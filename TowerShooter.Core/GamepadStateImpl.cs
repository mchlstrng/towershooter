using Microsoft.Xna.Framework.Input;

namespace TowerShooter
{
    public class GamepadStateImpl : IGamepadState
    {
        public GamePadState GetState()
        {
            return GamePad.GetState(0);
        }
    }

}