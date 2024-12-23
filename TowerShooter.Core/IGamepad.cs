namespace TowerShooter
{
    public interface IGamepad
    {
        bool IsButtonPressA();
        bool IsButtonSinglePressA();
        bool IsDpadPressDown();
        bool IsDpadPressLeft();
        bool IsDpadPressRight();
        void Update();
    }
}