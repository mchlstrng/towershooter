using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TowerShooter
{
    public interface IInputManager
    {
        bool IsKeyDown(Keys key);
        bool IsLeftClickDown();
        bool IsRightClickDown();
        bool IsSingleKeyPress(Keys key);
        bool IsSingleLeftClick();
        bool IsSingleRightClick();
        void Update(GameTime gameTime);
        bool IsScrollUp();
        bool IsScrollDown();
    }
}