using ItemRarities.Enums;
using ItemRarities.Utilities;
using Newtonsoft.Json.Linq;
using System.Collections;
using UnityEngine.Networking;

namespace ItemRarities;

public static class RarityManager
{
    internal static bool isInitialized;
    private static Dictionary<string, Rarities> raritiesLookup = new();

    private static IEnumerator AssignRarities()
    {
        var gearNames = new SortedSet<string>();
        var enumerator = ConsoleManager.m_SearchStringToGearNames.Values.GetEnumerator();

        while (enumerator.MoveNext())
        {
            var name = enumerator.Current;
            if (name != null && name.StartsWith("GEAR_"))
            {
                gearNames.Add(name);
            }
        }
        
        yield return null;
    }
    
    internal static Rarities GetRarity(string itemName) => raritiesLookup.GetValueOrDefault(itemName, Rarities.None);
    
    internal static IEnumerator InitializeRarities()
    {
        yield return LoadRaritiesFromLocalFile();
        yield return AssignRarities();
        isInitialized = true;
    }
    
    // This is basically the same logic when loading the Localizations.json
    // Move this into a common method in another Utils class, maybe.
    private static IEnumerator LoadRaritiesFromLocalFile()
    {
        const string itemRaritiesJson = "ItemRarities.Resources.ItemRarities.json";
        
        try
        {
            using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(itemRaritiesJson) ?? throw new InvalidOperationException($"Failed to load resource: {itemRaritiesJson}");
            using StreamReader reader = new(stream);

            var jsonText = reader.ReadToEnd();
            
            var jsonObject = JObject.Parse(jsonText);
            foreach (var rarityGroup in jsonObject)
            {
                if (!Enum.TryParse(rarityGroup.Key, out Rarities rarity) || rarityGroup.Value == null) continue;
                foreach (var item in rarityGroup.Value)
                {
                    raritiesLookup[item.ToString()] = rarity;
                }
            }
        }
        catch (Exception ex)
        {
            Logging.LogError($"Failed to parse JSON: {ex.Message}");
        }
        
        yield break;
    }
}