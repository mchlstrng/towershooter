using System.Collections.Generic;

namespace TowerShooter.Screens
{
    /// <summary>
    /// A setting item. A setting item has a name, a list of available values and a current value.
    /// </summary>
    public class SettingItem
    {
        /// <summary>
        /// The name of the setting.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The current value of the setting.
        /// </summary>
        public string CurrentValue => $"{AvailableValues[ValueIndex]}";

        /// <summary>
        /// The index of the current value in the list of available values.
        /// </summary>
        public int ValueIndex { get; set; }

        /// <summary>
        /// The list of available values.
        /// </summary>
        public IReadOnlyList<string> AvailableValues { get; set; } = new List<string>();

        /// <summary>
        /// Sets the next setting value.
        /// If the current value is the last value in the list of available values, the first value in the list of available values is set.
        /// </summary>
        public void SetNextSettingValue()
        {
            ValueIndex++;
            if (ValueIndex >= AvailableValues.Count)
            {
                ValueIndex = 0;
            }
        }
    }
}