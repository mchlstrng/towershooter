using Microsoft.Xna.Framework;
using Penumbra;

namespace TowerShooter
{
    public class NoFlashlight : IFlashlightComponent
    {
        public void AddLight(string name, Light light)
        {
        }

        public void BeginDraw()
        {
        }

        public void Disable()
        {
        }

        public void Enable()
        {
        }

        public Light GetLight<T>(string name)
        {
            return null;
        }

        public void Initialize()
        {
        }

        public void SetAmbientColor(Color color)
        {
        }

        public void SetMatrix(Matrix matrix)
        {
        }
    }
}