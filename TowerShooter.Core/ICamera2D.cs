using Microsoft.Xna.Framework;

namespace TowerShooter
{
    public interface ICamera2D
    {
        Vector2 Position { get; set; }

        float Rotation { get; set; }

        float Zoom { get; set; }

        Matrix GetTransformation();

        void Move(Vector2 amount);

        void ResetToNormal();

        Matrix ViewMatrix { get; set; }

        void Update();
    }
}