using Microsoft.Xna.Framework.Input;

namespace TowerShooter
{
    public interface IKeyboard
    {
        void Update();
        bool IsKeyDown(Keys key);
        bool IsSingleKeyPress(Keys key);
    }
}