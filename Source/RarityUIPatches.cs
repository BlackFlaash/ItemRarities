using ItemRarities.Enums;
using ItemRarities.Managers;
using ItemRarities.Utilities;

namespace ItemRarities;

internal static class RarityUIPatches
{
    // This logic for the clothing is very work-in-progress.
    // It works, however there are some bugs which I need to further diagnose.
    
    // [HarmonyPatch(typeof(ClothingSlot), nameof(ClothingSlot.SetSelected))]
    // private static class Testing1
    // {
    //     private static void Postfix(ClothingSlot __instance, bool isSelected)
    //     {
    //         if (__instance.m_GearItem == null) return;
    //         
    //         foreach (Transform child in __instance.m_Selected.transform)
    //         {
    //             if (child.gameObject.name == "InnerGlow")
    //             {
    //                 child.GetComponent<UISprite>().color = RarityUIManager.GetRarityAndColour(__instance.m_GearItem);
    //             }
    //             if (child.gameObject.name == "TweenedContent")
    //             {
    //                 child.GetComponent<UISprite>().color = RarityUIManager.GetRarityAndColour(__instance.m_GearItem);
    //             }
    //         }
    //     }
    // }
    //
    // [HarmonyPatch(typeof(ClothingSlot), nameof(ClothingSlot.ActivateMouseHoverHighlight))]
    // private static class Testing2
    // {
    //     private static void Postfix(ClothingSlot __instance)
    //     {
    //         if (__instance.m_GearItem == null) return;
    //         __instance.m_SpriteBoxHover.GetComponent<UISprite>().color = RarityUIManager.GetRarityAndColour(__instance.m_GearItem, 0.5f);
    //
    //         foreach (Transform child in __instance.transform)
    //         {
    //             if (child.gameObject.name != "Button") continue;
    //             child.GetComponent<UIWidget>().color = RarityUIManager.GetRarityAndColour(__instance.m_GearItem);
    //             child.GetComponent<UIButton>().hover = RarityUIManager.GetRarityAndColour(__instance.m_GearItem, 0.4f);
    //             child.GetComponent<UIButton>().pressed = RarityUIManager.GetRarityAndColour(__instance.m_GearItem, 0.6f);
    //         }
    //     }
    // }
    
    [HarmonyPatch(typeof(InventoryGridItem), nameof(InventoryGridItem.OnHover))]
    private static class UpdateInventoryGridItemHoverRarityColour
    {
        private static void Postfix(InventoryGridItem __instance, bool isOver)
        {
            __instance.m_Button.hover = RarityUIManager.GetRarityAndColour(__instance.m_GearItem, 0.5f);
            __instance.m_Button.pressed = RarityUIManager.GetRarityAndColour(__instance.m_GearItem, 0.5f);
        }
    }
    
    [HarmonyPatch(typeof(Panel_Inventory), nameof(Panel_Inventory.Initialize))]
    private static class InitializeSortRarityButton
    {
        private static void Postfix(Panel_Inventory __instance)
        {
            UIButtonExtensions.NewSortButton(__instance.m_SortButtons, "Button_SortRarity", "ico_Star", __instance.OnSortChange, 85, 1.8f);
            __instance.m_SortFlipDictionary.Add("GAMEPLAY_SortRarity", false);
        }
    }
    
    [HarmonyPatch(typeof(Panel_Inventory), nameof(Panel_Inventory.OnSortChange))]
    private static class SortChangeRarityPatch
    {
        private static void Prefix(Panel_Inventory __instance, UIButton sortButtonClicked)
        {
            if (sortButtonClicked.name == "Button_SortRarity")
            {
                if (__instance.m_SortName == "GAMEPLAY_SortRarity")
                {
                    RarityUIManager.ToggleRaritySort();
                }
            }
        }
    }

    [HarmonyPatch(typeof(Panel_Inventory), nameof(Panel_Inventory.UpdateFilteredInventoryList))]
    private static class SortByRaritiesPatch
    {
        private static void Postfix(Panel_Inventory __instance)
        {
            if (__instance.m_SortName == "GAMEPLAY_SortRarity")
            {
                RarityUIManager.CompareGearByRarity(__instance);
            }
        }
    }
    
    [HarmonyPatch(typeof(Panel_Inventory), nameof(Panel_Inventory.UpdateGearStatsBlock))]
    private static class UpdatePanelInventorySelectedRarityColour
    {
        private static void Postfix(Panel_Inventory __instance)
        {
            __instance.m_SelectedSpriteObj.GetComponentInChildren<UISprite>().color = RarityUIManager.GetRarityAndColour(__instance.GetCurrentlySelectedGearItem());
            __instance.m_SelectedSpriteTweenScale.GetComponent<UISprite>().color = RarityUIManager.GetRarityAndColour(__instance.GetCurrentlySelectedGearItem());
        }
    }
}