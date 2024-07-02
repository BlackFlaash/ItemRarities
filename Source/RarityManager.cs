using ItemRarities.Enums;
using ItemRarities.Utilities;
using Newtonsoft.Json.Linq;
using System.Collections;
using UnityEngine.Networking;

namespace ItemRarities;

public static class RarityManager
{
    private static bool isInitialized;
    internal static Dictionary<string, Rarities> rarityLookup = new();
    private const string GistUrl = "https://gist.githubusercontent.com/Deaadman/88a0f2f8bcc27c07a81331a4714de5c2/raw/95a79002dbcc51c6be40ebfc43090574fd4927fd/ItemRarities.json";

    internal static IEnumerator AssignRarities()
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
        
        // foreach (var gearName in gearNames)
        // {
        //     var gearItem = GearItem.LoadGearItemPrefab(gearName);
        //     if (gearItem == null) continue;
        //
        //     var itemRarity = gearItem.GetComponent<ItemRarity>() ?? gearItem.gameObject.AddComponent<ItemRarity>();
        //     if (rarityLookup.TryGetValue(gearName, out var rarity))
        //     {
        //         itemRarity.Rarity = rarity;
        //         // Logging.Log($"{gearName} was assigned the rarity: {rarity}");
        //     }
        //     else
        //     {
        //         Logging.LogWarning($"No rarity found for {gearName}");
        //     }
        //
        //     
        // }
        
        yield return null;
    }

    internal static Rarities GetRarity(string itemName) => rarityLookup.TryGetValue(itemName, out var rarity) ? rarity : Rarities.Common;
    
    internal static IEnumerator InitializeRarities()
    {
        yield return LoadRaritiesFromGist();
        yield return AssignRarities();
        isInitialized = true;
    }

    internal static bool IsInitialized() => isInitialized;
    
    private static IEnumerator LoadRaritiesFromGist()
    {
        var www = UnityWebRequest.Get(GistUrl);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Logging.LogError($"Failed to load item rarities: {www.error}");
            yield break;
        }

        try
        {
            var jsonObject = JObject.Parse(www.downloadHandler.text);
            foreach (var rarityGroup in jsonObject)
            {
                if (!Enum.TryParse(rarityGroup.Key, out Rarities rarity) || rarityGroup.Value == null) continue;
                foreach (var item in rarityGroup.Value)
                {
                    rarityLookup[item.ToString()] = rarity;
                }
            }
        }
        catch (Exception ex)
        {
            Logging.LogError($"Failed to parse JSON: {ex.Message}");
        }
    }
}