using ItemRarities.Utilities;
using LocalizationUtilities;

namespace ItemRarities;

internal sealed class Mod : MelonMod
{
    public override void OnInitializeMelon()
    {
        LoadLocalizations();
    }

    // Uncomment this when required, e.g. after a new update drops to see which items don't have a rarity.
    // public override void OnSceneWasInitialized(int buildIndex, string sceneName)
    // {
    //     Logging.LogItemsWithoutRarities();
    // }

    public override void OnSceneWasLoaded(int buildIndex, string sceneName)
    {
        if (RarityManager.isInitialized) return;
        if (!GameManager.IsMainMenuActive()) return;
        
        MelonCoroutines.Start(RarityManager.InitializeRarities());
    }
    
    private static void LoadLocalizations()
    {
        const string localizationJson = "ItemRarities.Resources.Localization.json";

        try
        {
            using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(localizationJson) ?? throw new InvalidOperationException($"Failed to load resource: {localizationJson}");
            using StreamReader reader = new(stream);

            var jsonText = reader.ReadToEnd();

            LocalizationManager.LoadJsonLocalization(jsonText);
        }
        catch (Exception ex)
        {
            Logging.LogError(ex.Message);
        }
    }
}