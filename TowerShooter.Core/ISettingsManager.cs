using TowerShooter.Screens;

namespace TowerShooter
{
    public interface ISettingsManager
    {
        Settings GetSettings();
        void SaveSettings(Settings settings);
        void SetIsCreative();
    }
}