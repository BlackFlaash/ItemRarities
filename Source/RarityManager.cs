using ItemRarities.Enums;
using ItemRarities.Utilities;
using Newtonsoft.Json.Linq;
using System.Collections;

namespace ItemRarities;

public static class RarityManager
{
    internal static bool isInitialized;
    private static Dictionary<string, Rarities> raritiesLookup = new();

    /// <summary>
    /// Adds a Rarity to your custom GearItem.
    /// </summary>
    /// <param name="gearItem">The name of the GearItem to add.</param>
    /// <param name="rarity">The rarity of the GearItem, represented by the Rarities enum.</param>
    public static void AddGearItemAndRarity(string gearItem, Rarities rarity)
    {
        raritiesLookup[gearItem] = rarity;
    }
    
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
    
    private static IEnumerator LoadRaritiesFromLocalFile()
    {
        JsonUtilities.ReadJSON("ItemRarities.Resources.ItemRarities.json", out var jsonText);
        
        var jsonObject = JObject.Parse(jsonText);
        foreach (var rarityGroup in jsonObject)
        {
            if (!Enum.TryParse(rarityGroup.Key, out Rarities rarity) || rarityGroup.Value == null) continue;
            foreach (var item in rarityGroup.Value)
            {
                raritiesLookup[item.ToString()] = rarity;
            }
        }
        
        yield break;
    }
}