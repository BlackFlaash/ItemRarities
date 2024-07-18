using ItemRarities.Managers;
using ItemRarities.Utilities;

namespace ItemRarities;

internal static class RarityUIPatches
{
    [HarmonyPatch(typeof(ClothingSlot), nameof(ClothingSlot.ActivateMouseHoverHighlight))]
    private static class UpdateClothingSlotItemHoverRarityColour
    {
        private static void Postfix(ClothingSlot __instance)
        {
            RarityUIManager.UpdateClothingSlotColors(__instance);
        }
    }
    
    [HarmonyPatch(typeof(ClothingSlot), nameof(ClothingSlot.SetSelected))]
    private static class UpdateClothingSlotSelectedRarityColour
    {
        private static void Postfix(ClothingSlot __instance, bool isSelected)
        {
            RarityUIManager.UpdateClothingSlotColors(__instance);
        }
    }
    
    // This needs a bit work, the hover affect looks strange when an item doesn't have a rarity assigned.
    [HarmonyPatch(typeof(InventoryGridItem), nameof(InventoryGridItem.OnHover))]
    private static class UpdateInventoryGridItemHoverRarityColour
    {
        private static void Postfix(InventoryGridItem __instance, bool isOver)
        {
            if (__instance.m_Button == null || __instance.m_GearItem == null) return;
            __instance.m_Button.hover = RarityUIManager.GetRarityAndColour(__instance.m_GearItem, 0.5f);
            __instance.m_Button.pressed = RarityUIManager.GetRarityAndColour(__instance.m_GearItem, 0.5f, 0.5f);
        }
    }
    
    [HarmonyPatch(typeof(Panel_Clothing), nameof(Panel_Clothing.Enable))]
    private static class UpdatePanelClothingRarityColourImmediately
    {
        private static void Postfix(Panel_Clothing __instance)
        {
            foreach (var slot in __instance.m_ClothingSlots)
            {
                RarityUIManager.UpdateClothingSlotColors(slot);
            }
        }
    }

    [HarmonyPatch(typeof(Panel_Clothing), nameof(Panel_Clothing.OnUseClothingItem))]
    private static class UpdatePanelClothingSelectedRarityColour
    {
        private static void Postfix(Panel_Clothing __instance)
        {
            // Need this to properly update the colours for some reason?
            MelonCoroutines.Start(RarityUIManager.DelayedUpdateAllSlots(__instance));
        }
    }
    
    [HarmonyPatch(typeof(Panel_Container), nameof(Panel_Container.Initialize))]
    private static class InitializeSortRarityButtonContainer
    {
        private static void Postfix(Panel_Container __instance)
        {
            UIButtonExtensions.NewSortButton(__instance.m_SortButtons, "Button_SortRarity", "ico_Star", __instance.OnSortInventoryChange, 100, 1.8f);
            __instance.m_SortFlipDictionary.Add("GAMEPLAY_SortRarity", false);
        }
    }
    
    [HarmonyPatch(typeof(Panel_Container), nameof(Panel_Container.OnSortInventoryChange))]
    private static class SortChangeRarityPatchContainer
    {
        private static void Prefix(Panel_Container __instance, UIButton sortButtonClicked)
        {
            if (sortButtonClicked.name == "Button_SortRarity")
            {
                if (__instance.m_InventorySortName == "GAMEPLAY_SortRarity")
                {
                    RarityUIManager.ToggleRaritySort();
                }
            }
        }
    }

    [HarmonyPatch(typeof(Panel_Container), nameof(Panel_Container.UpdateFilteredContainerList))]
    private static class SortByRaritiesPatchContainerList
    {
        private static void Postfix(Panel_Container __instance)
        {
            if (__instance.m_InventorySortName == "GAMEPLAY_SortRarity")
            {
                RarityUIManager.CompareGearByRarity(__instance, false);
            }
        }
    }
    
    [HarmonyPatch(typeof(Panel_Container), nameof(Panel_Container.UpdateFilteredInventoryList))]
    private static class SortByRaritiesPatchContainerInventoryList
    {
        private static void Postfix(Panel_Container __instance)
        {
            if (__instance.m_InventorySortName == "GAMEPLAY_SortRarity")
            {
                RarityUIManager.CompareGearByRarity(__instance);
            }
        }
    }
    
    [HarmonyPatch(typeof(Panel_Inventory), nameof(Panel_Inventory.Initialize))]
    private static class InitializeSortRarityButtonInventory
    {
        private static void Postfix(Panel_Inventory __instance)
        {
            UIButtonExtensions.NewSortButton(__instance.m_SortButtons, "Button_SortRarity", "ico_Star", __instance.OnSortChange, 85, 1.8f);
            __instance.m_SortFlipDictionary.Add("GAMEPLAY_SortRarity", false);
        }
    }
    
    [HarmonyPatch(typeof(Panel_Inventory), nameof(Panel_Inventory.OnSortChange))]
    private static class SortChangeRarityPatchInventory
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
    private static class SortByRaritiesPatchInventory
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
            if (__instance.m_SelectedSpriteObj == null || __instance.m_SelectedSpriteTweenScale == null) return;
            __instance.m_SelectedSpriteObj.GetComponentInChildren<UISprite>().color = RarityUIManager.GetRarityAndColour(__instance.GetCurrentlySelectedGearItem(), 1, 0.5f);
            __instance.m_SelectedSpriteTweenScale.GetComponent<UISprite>().color = RarityUIManager.GetRarityAndColour(__instance.GetCurrentlySelectedGearItem(), 1, 1);
        }
    }
}