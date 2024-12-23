using Microsoft.Xna.Framework;
using Penumbra;
using System.Collections.Generic;

namespace TowerShooter
{
    public class PenumbraWrapper : IFlashlightComponent
    {
        private readonly Dictionary<string, Light> Lights = new();

        private readonly PenumbraComponent penumbra;

        public PenumbraWrapper(TowerShooter towerShooter)
        {
            penumbra = new PenumbraComponent(towerShooter);
            towerShooter.Components.Add(penumbra);
        }

        public void Initialize()
        {
            penumbra.Initialize();
        }

        public void BeginDraw()
        {
            penumbra.BeginDraw();
        }

        public void SetMatrix(Matrix matrix)
        {
            penumbra.Transform = matrix;
        }

        public void AddLight(string name, Light light)
        {
            Lights.Add(name, light);
            penumbra.Lights.Add(light);
        }

        public Light GetLight<T>(string name)
        {
            bool canGetValue = Lights.TryGetValue(name, out Light lightType);

            if (canGetValue)
                return lightType;

            return null;
        }

        public void SetAmbientColor(Color color)
        {
            penumbra.AmbientColor = color;
        }

        public void Disable()
        {
            penumbra.Visible = false;
            penumbra.Enabled = false;
        }

        public void Enable()
        {
            penumbra.Enabled = true;
            penumbra.Visible = true;
        }
    }
}