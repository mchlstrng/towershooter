using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TowerShooter
{
    public interface IMouse
    {
        void Update();
        Rectangle GetMouseRectangle();
        bool IsLeftClickDown();
        bool IsRightClickDown();
        bool IsSingleLeftClick();
        bool IsSingleRightClick();
        bool IsScrollUp();
        bool IsScrollDown();
    }
}