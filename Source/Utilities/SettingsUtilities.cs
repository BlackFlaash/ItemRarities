using ItemRarities.Properties;

namespace ItemRarities.Utilities;

internal static class SettingsUtilities
{
    internal static void UpdateFieldVisibilities(bool visible)
    {
        Settings.Instance.SetFieldVisible(nameof(Settings.Instance.commonRed), visible);
        Settings.Instance.SetFieldVisible(nameof(Settings.Instance.commonGreen), visible);
        Settings.Instance.SetFieldVisible(nameof(Settings.Instance.commonBlue), visible);
        
        Settings.Instance.SetFieldVisible(nameof(Settings.Instance.uncommonRed), visible);
        Settings.Instance.SetFieldVisible(nameof(Settings.Instance.uncommonGreen), visible);
        Settings.Instance.SetFieldVisible(nameof(Settings.Instance.uncommonBlue), visible);
        
        Settings.Instance.SetFieldVisible(nameof(Settings.Instance.rareRed), visible);
        Settings.Instance.SetFieldVisible(nameof(Settings.Instance.rareGreen), visible);
        Settings.Instance.SetFieldVisible(nameof(Settings.Instance.rareBlue), visible);
        
        Settings.Instance.SetFieldVisible(nameof(Settings.Instance.epicRed), visible);
        Settings.Instance.SetFieldVisible(nameof(Settings.Instance.epicGreen), visible);
        Settings.Instance.SetFieldVisible(nameof(Settings.Instance.epicBlue), visible);
        
        Settings.Instance.SetFieldVisible(nameof(Settings.Instance.legendaryRed), visible);
        Settings.Instance.SetFieldVisible(nameof(Settings.Instance.legendaryGreen), visible);
        Settings.Instance.SetFieldVisible(nameof(Settings.Instance.legendaryBlue), visible);
        
        Settings.Instance.SetFieldVisible(nameof(Settings.Instance.mythicRed), visible);
        Settings.Instance.SetFieldVisible(nameof(Settings.Instance.mythicGreen), visible);
        Settings.Instance.SetFieldVisible(nameof(Settings.Instance.mythicBlue), visible);
    }
}