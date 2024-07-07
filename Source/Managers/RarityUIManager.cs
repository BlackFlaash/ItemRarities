using ItemRarities.Components;
using ItemRarities.Enums;
using ItemRarities.Properties;
using ItemRarities.Utilities;

namespace ItemRarities.Managers;

internal static class RarityUIManager
{
    internal static UILabel? m_RarityLabel;
    internal static UILabel? m_RarityLabelInspect;

    internal static Color GetRarityAndColour(GearItem gearItem, float alpha = 1f) => gearItem == null ? Color.clear : GetRarityColour(RarityManager.GetRarity(gearItem.name), alpha);
    
    private static Color GetRarityColour(Rarities rarity, float alpha = 1f)
    {
        if (Settings.Instance.customColours)
        {
            return rarity switch
            {
                Rarities.Common => new Color(Settings.Instance.commonRed, Settings.Instance.commonGreen, Settings.Instance.commonBlue, alpha),
                Rarities.Uncommon => new Color(Settings.Instance.uncommonRed, Settings.Instance.uncommonGreen, Settings.Instance.uncommonBlue, alpha),
                Rarities.Rare => new Color(Settings.Instance.rareRed, Settings.Instance.rareGreen, Settings.Instance.rareBlue, alpha),
                Rarities.Epic => new Color(Settings.Instance.epicRed, Settings.Instance.epicGreen, Settings.Instance.epicBlue, alpha),
                Rarities.Legendary => new Color(Settings.Instance.legendaryRed, Settings.Instance.legendaryGreen, Settings.Instance.legendaryBlue, alpha),
                Rarities.Mythic => new Color(Settings.Instance.mythicRed, Settings.Instance.mythicGreen, Settings.Instance.mythicBlue, alpha),
                _ => Color.clear
            };
        }
        return rarity switch
        {
            Rarities.Common => new Color(0.6f, 0.6f, 0.6f, alpha),
            Rarities.Uncommon => new Color(0.3f, 0.7f, 0f, alpha),
            Rarities.Rare => new Color(0f, 0.6f, 0.9f, alpha),
            Rarities.Epic => new Color(0.7f, 0.3f, 0.9f, alpha),
            Rarities.Legendary => new Color(0.9f, 0.5f, 0.2f, alpha),
            Rarities.Mythic => new Color(0.8f, 0.7f, 0.3f, alpha),
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
    
    internal static void UpdateRarityLabelProperties(GearItem gearItem, bool inspectLabel = false)
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
            rarityLabelType.gameObject.SetActive(true);
            
            var existingShiny = rarityLabelType.GetComponent<MythicGlowEffect>();
            if (existingShiny != null)
            {
                UnityEngine.Object.Destroy(existingShiny);
            }
            if (rarity == Rarities.Mythic)
            {
                rarityLabelType.gameObject.AddComponent<MythicGlowEffect>();
            }
            
            rarityLabelType.color = GetRarityColour(rarity);
        }
    }
}