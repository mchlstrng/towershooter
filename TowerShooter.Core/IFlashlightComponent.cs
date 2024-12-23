using Microsoft.Xna.Framework;
using Penumbra;

namespace TowerShooter
{
    public interface IFlashlightComponent
    {
        void AddLight(string name, Light light);
        void BeginDraw();
        Light GetLight<T>(string name);
        void Initialize();
        void SetAmbientColor(Color color);
        void SetMatrix(Matrix matrix);
        void Disable();
        void Enable();
    }
}