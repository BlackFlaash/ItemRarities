using ItemRarities.Enums;
using ItemRarities.Utilities;

namespace ItemRarities;

internal static class RarityUIManager
{
    private static UILabel? m_RarityLabel;

    private static Color GetRarityColor(Rarities rarity)
    {
        return rarity switch
        {
            Rarities.Common => Color.grey,
            Rarities.Uncommon => Color.green,
            Rarities.Rare => Color.blue,
            Rarities.Epic => Color.magenta,
            Rarities.Legendary => Color.yellow,
            _ => Color.black
        };
    }
    
    internal static void InstantiateOrMoveUILabel(Transform transform, float posX, float posY, float posZ)
    {
        if (m_RarityLabel == null)
        {
            m_RarityLabel = UIUtilities.SetupGameObjectUILabel("RarityLabel", transform, false, posX, posY, posZ);
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
                Color.white,
                true
            );
        }
        else
        {
            m_RarityLabel.transform.SetParent(transform, false);
            m_RarityLabel.transform.localPosition = new Vector3(posX, posY, posZ);
        }
    }
    
    internal static void UpdateRarityTextAndColour(Rarities rarity)
    {
        if (m_RarityLabel == null) return;
        if (rarity == Rarities.None)
        {
            m_RarityLabel.text = string.Empty;
            m_RarityLabel.gameObject.SetActive(false);
        }
        else
        {
            m_RarityLabel.text = rarity.ToString();
            m_RarityLabel.color = GetRarityColor(rarity);
            m_RarityLabel.gameObject.SetActive(true);
        }
    }

    internal static void UpdateRarityUILabelVisibility(bool isActive) => m_RarityLabel.gameObject.SetActive(isActive);
}