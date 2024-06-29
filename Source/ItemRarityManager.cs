using ItemRarities.Components;
using ItemRarities.Enums;
using ItemRarities.Utilities;
using Newtonsoft.Json.Linq;
using System.Collections;
using UnityEngine.Networking;

namespace ItemRarities;

public static class ItemRarityManager
{
    private static bool isInitialized = false;
    private static Dictionary<string, Rarities> rarityLookup = new();
    private const string GistUrl = "https://gist.githubusercontent.com/Deaadman/88a0f2f8bcc27c07a81331a4714de5c2/raw/95a79002dbcc51c6be40ebfc43090574fd4927fd/ItemRarities.json";

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

        foreach (var gearName in gearNames)
        {
            var gearItem = GearItem.LoadGearItemPrefab(gearName);
            if (gearItem == null) continue;

            var itemRarity = gearItem.gameObject.GetComponent<GearItemRarity>() ?? gearItem.gameObject.AddComponent<GearItemRarity>();
            if (rarityLookup.TryGetValue(gearName, out var rarity))
            {
                itemRarity.ItemRarity = rarity;
            }
            else
            {
                Logging.LogWarning($"No rarity found for {gearName}");
            }

            yield return null;
        }
    }

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
    
    #region Uncomment These When Required Later
    // private static ItemRarity.ItemRarities GetRarity(string itemName) => rarityLookup.TryGetValue(itemName, out var rarity) ? rarity : ItemRarity.ItemRarities.Common;

    // public static void SetCustomRarity(GearItem gearItem, GearItemRarity.ItemRarities rarity)
    // {
    //     if (gearItem == null) return;
    //
    //     var itemRarity = gearItem.gameObject.GetComponent<ItemRarity>() ?? gearItem.gameObject.AddComponent<ItemRarity>();
    //     itemRarity.m_ItemRarities = rarity;
    //     rarityLookup[gearItem.name] = rarity;
    // }
    #endregion
}