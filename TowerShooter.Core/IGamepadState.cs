using Microsoft.Xna.Framework.Input;

namespace TowerShooter
{
    public interface IGamepadState
    {
        GamePadState GetState();
    }
}