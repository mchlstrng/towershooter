using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TowerShooter
{
    public class MouseStateImpl : IMouseStateImpl
    {
        public MouseState GetState()
        {
            return Mouse.GetState();
        }
    }
}