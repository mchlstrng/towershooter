using Microsoft.Xna.Framework;

namespace TowerShooter
{
    public class Camera2D : ICamera2D
    {
        public Matrix ViewMatrix { get; set; }

        private readonly ITowerShooter towerShooter;

        private float _zoom;
        public float Zoom
        {
            get { return _zoom; }
            set
            {
                _zoom = value;

                // Negative zoom will flip image
                if (_zoom < 0.1f)
                {
                    _zoom = 0.1f;
                }
            }
        }

        public float Rotation { get; set; }

        public Vector2 Position { get; set; }

        public Camera2D(ITowerShooter towerShooter)
        {
            this.towerShooter = towerShooter;
            ResetToNormal();
        }

        public void ResetToNormal()
        {
            Position = new Vector2(towerShooter.ResolutionX / 2f, towerShooter.ResolutionY / 2f);
            Zoom = 1;
        }

        public void Move(Vector2 amount)
        {
            Position += amount;
        }

        public Matrix GetTransformation()
        {
            ViewMatrix =
              Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
                                         Matrix.CreateRotationZ(Rotation) *
                                         Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                                         Matrix.CreateTranslation(new Vector3(towerShooter.GraphicsDeviceManager.GraphicsDevice.Viewport.Width * 0.5f, towerShooter.GraphicsDeviceManager.GraphicsDevice.Viewport.Height * 0.5f, 0));
            return ViewMatrix;
        }

        public void Update()
        {
            if (towerShooter.Debug)
            {
                if (towerShooter.InputManager.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Q))
                    Zoom += 0.05f;
                if (towerShooter.InputManager.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Z))
                    Zoom -= 0.05f;
            }

            Zoom = MathHelper.Clamp(Zoom, 1, 4);
        }
    }
}