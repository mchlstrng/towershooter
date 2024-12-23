namespace TowerShooter
{
    public class DebugService : IDebugService
    {
        private readonly ITowerShooter towerShooter;

        public DebugService(ITowerShooter towerShooter)
        {
            this.towerShooter = towerShooter ?? throw new System.ArgumentNullException(nameof(towerShooter));
        }

        public void Update()
        {
            if (towerShooter.InputManager.IsSingleKeyPress(Microsoft.Xna.Framework.Input.Keys.F3))
            {
                towerShooter.Debug = !towerShooter.Debug;
            }
        }
    }
}