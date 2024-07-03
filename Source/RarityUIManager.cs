using ItemRarities.Enums;
using ItemRarities.Utilities;

namespace ItemRarities;

internal static class RarityUIManager
{
    internal static UILabel? m_RarityLabel;
    internal static UILabel? m_RarityLabelInspect;

    private static Color GetRarityColour(Rarities rarity)
    {
        return rarity switch
        {
            Rarities.Common => new Color(0.6f, 0.6f, 0.6f),
            Rarities.Uncommon => new Color(0.3f, 0.7f, 0f),
            Rarities.Rare => new Color(0f, 0.6f, 0.9f),
            Rarities.Epic => new Color(0.7f, 0.3f, 0.9f),
            Rarities.Legendary => new Color(0.9f, 0.5f, 0.2f),
            Rarities.Mythic => new Color(0.8f, 0.7f, 0.3f),
            _ => Color.clear
        };
    }

    private static string GetRarityLocalizationKey(Rarities rarity)
    {
        return rarity switch
        {
            Rarities.Common => Localization.Get("GAMEPLAY_RarityCommon"),
            Rarities.Uncommon => Localization.Get("GAMEPLAY_RarityUncommon"),
            Rarities.Rare => Localization.Get("GAMEPLAY_RarityRare"),
            Rarities.Epic => Localization.Get("GAMEPLAY_RarityEpic"),
            Rarities.Legendary => Localization.Get("GAMEPLAY_RarityLegendary"),
            Rarities.Mythic => Localization.Get("GAMEPLAY_RarityMythic"),
            _ => string.Empty
        };
    }

    internal static void InstantiateInspectRarityLabel(Transform transform)
    {
        var gameObject = new GameObject("RarityLabelInspectGameObject");
        gameObject.transform.SetParent(transform, false);
        gameObject.transform.localPosition = new Vector3(0, 0, 0);
        
        m_RarityLabelInspect = UIUtilities.SetupGameObjectWithUILabel("RarityLabelInspect", gameObject.transform, false, 40, 0, 0);
        UIUtilities.SetupUILabel(
            m_RarityLabelInspect,
            string.Empty,
            FontStyle.Normal,
            UILabel.Crispness.Always,
            NGUIText.Alignment.Left,
            UILabel.Overflow.ResizeHeight,
            true,
            2,
            14,
            Color.clear,
            true
        );
        
        m_RarityLabelInspect.transform.SetSiblingIndex(0);
    }
    
    internal static void InstantiateOrMoveRarityLabel(Transform transform, float posX, float posY, float posZ)
    {
        if (m_RarityLabel == null)
        {
            m_RarityLabel = UIUtilities.SetupGameObjectWithUILabel("RarityLabel", transform, false, posX, posY, posZ);
            UIUtilities.SetupUILabel(
                m_RarityLabel,
                string.Empty,
                FontStyle.Normal,
                UILabel.Crispness.Always,
                NGUIText.Alignment.Center,
                UILabel.Overflow.ResizeHeight,
                true,
                0,
                18,
                Color.clear,
                true
            );
        }
        else
        {
            m_RarityLabel.transform.SetParent(transform, false);
            m_RarityLabel.transform.localPosition = new Vector3(posX, posY, posZ);
        }
    }
    
    internal static void UpdateRarityLabelProperties(GearItem gearItem, bool inspectLabel)
    {
        if (gearItem == null) return;
        
        var rarityLabelType = inspectLabel ? m_RarityLabelInspect : m_RarityLabel;
        var rarity = RarityManager.GetRarity(gearItem.name);
        
        if (rarityLabelType == null) return;
        
        if (rarity == Rarities.None)
        {
            rarityLabelType.text = GetRarityLocalizationKey(rarity);
            rarityLabelType.gameObject.SetActive(false);
        }
        else
        {
            rarityLabelType.text = GetRarityLocalizationKey(rarity);
            rarityLabelType.color = GetRarityColour(rarity);
            rarityLabelType.gameObject.SetActive(true);
        }
    }
}