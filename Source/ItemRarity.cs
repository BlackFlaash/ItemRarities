using ItemRarities.Utilities;
using Newtonsoft.Json.Linq;

namespace ItemRarities;

[RegisterTypeInIl2Cpp(false)]
public class ItemRarity : MonoBehaviour
{
    public enum ItemRarities
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary,
        Mythic
    }

    public ItemRarities m_ItemRarities;
    private static Dictionary<string, ItemRarities>? SRarityLookup;

    static ItemRarity()
    {
        const string resourceName = "ItemRarities.Resources.ItemRarities.json";
        var assembly = Assembly.GetExecutingAssembly();

        using var stream = assembly.GetManifestResourceStream(resourceName);
        if (stream == null) return;
        using var reader = new StreamReader(stream);
        var jsonText = reader.ReadToEnd();
        var jsonObject = JObject.Parse(jsonText);
        SRarityLookup = new Dictionary<string, ItemRarities>();

        foreach (var rarityGroup in jsonObject)
        {
            if (!Enum.TryParse(rarityGroup.Key, out ItemRarities rarity)) continue;
            if (rarityGroup.Value == null) continue;
            foreach (var item in rarityGroup.Value)
            {
                SRarityLookup[item.ToString()] = rarity;
            }
        }
    }
    
    private void AssignRarities()
    {
        var gearItem = GetComponent<GearItem>();
        if (gearItem != null)
        {
            var itemName = gearItem.name;
            if (SRarityLookup != null && SRarityLookup.TryGetValue(itemName, out var rarity))
            {
                m_ItemRarities = rarity;
                Logging.Log($"Rarity {rarity} assigned to {itemName}");
            }
            else
            {
                Logging.Log($"No rarity found for {itemName}");
            }
        }
        else
        {
            Logging.Log("No GearItem component found on this object");
        }
    }

    private void Awake() => AssignRarities();
}