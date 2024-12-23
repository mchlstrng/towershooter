using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TowerShooter.Screens;

namespace TowerShooter
{
    public class SettingsManager : ISettingsManager
    {
        private readonly ITowerShooter towerShooter;

        public SettingsManager(ITowerShooter towerShooter)
        {
            this.towerShooter = towerShooter ?? throw new ArgumentNullException(nameof(towerShooter));
        }

        public static string SettingsFilePath()
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return Path.Combine(appData, "TowerShooterSettings.json");
        }

        public Settings GetSettings()
        {
            string settingsFile = SettingsFilePath();

            if (File.Exists(settingsFile))
            {
                string settingsAsText = File.ReadAllText(settingsFile);
                return JsonConvert.DeserializeObject<Settings>(settingsAsText);
            }
            else
            {
                return SetupDefaultSettings();
            }
        }

        public void SaveSettings(Settings settings)
        {
            string settingsAsText = JsonConvert.SerializeObject(settings);
            File.WriteAllText(SettingsFilePath(), settingsAsText);
        }

        public void SetIsCreative()
        {
            Settings settings = GetSettings();
            string creativeMode = settings.SettingItems.Single(s => s.Name == SettingNames.CreativeMode).CurrentValue;
            if (creativeMode == "Enabled")
                towerShooter.IsCreative = true;
            else
                towerShooter.IsCreative = false;
        }

        private static Settings SetupDefaultSettings()
        {
            return new Settings
            {
                SettingItems = new List<SettingItem>
                {
                    new SettingItem { Name = "Creative mode", ValueIndex = 0, AvailableValues = new List<string> { "Enabled", "Disabled" } }
                }
            };
        }
    }
}