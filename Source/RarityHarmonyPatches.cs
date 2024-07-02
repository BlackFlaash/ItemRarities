using Il2CppTLD.Cooking;

namespace ItemRarities;

// Need to organise these harmony patches later, from A-Z and their class names
internal static class RarityHarmonyPatches
{
    [HarmonyPatch(typeof(ItemDescriptionPage), nameof(ItemDescriptionPage.UpdateGearItemDescription))]
    private static class Testing1
    {
        private static void Postfix(ItemDescriptionPage __instance, GearItem gi)
        {
            if (gi == null) return;
            RarityUIManager.InstantiateOrMoveUILabel(__instance.m_ItemNameLabel.gameObject.transform, 0, 35, 0);
            RarityUIManager.UpdateRarityTextAndColour(RarityManager.GetRarity(gi.name));
        }
    }
    
    [HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.RefreshMainWindow))]
    private static class Testing2
    {
        private static void Postfix(Panel_Inventory_Examine __instance)
        {
            if (__instance.m_GearItem == null) return;
            RarityUIManager.InstantiateOrMoveUILabel(__instance.m_Item_Label.gameObject.transform, 0, 35, 0);
            RarityUIManager.UpdateRarityTextAndColour(RarityManager.GetRarity(__instance.m_GearItem.name));
        }
    }
    
    // Crafting and blueprints were recently changed, so I need to figure out how to get the currently selected gearitem now.
    // [HarmonyPatch(typeof(Panel_Crafting), nameof(Panel_Crafting.RefreshSelectedBlueprint))]
    // private static class Testing3
    // {
    //     private static void Postfix(Panel_Crafting __instance)
    //     {
    //         var gearItem = __instance.m_FilteredBlueprints[__instance].m_CraftedResult; 
    //         
    //         RarityUIManager.InstantiateOrMoveUILabel(__instance.m_SelectedName.gameObject.transform, 0, 35, 0);
    //         RarityUIManager.UpdateRarityTextAndColour(RarityManager.GetRarity(__instance));
    //     }
    // }
    
    [HarmonyPatch(typeof(Panel_Cooking), nameof(Panel_Cooking.GetSelectedCookableItem))]
    private static class Testing4
    {
        private static void Postfix(Panel_Cooking __instance, ref CookableItem __result)
        {
            if (__result.m_GearItem == null) return;
            RarityUIManager.InstantiateOrMoveUILabel(__instance.m_Label_CookedItemName.gameObject.transform, 0, 35, 0);
            RarityUIManager.UpdateRarityTextAndColour(RarityManager.GetRarity(__result.m_GearItem.name));
        }
    }
    
    [HarmonyPatch(typeof(Panel_Milling), nameof(Panel_Milling.GetSelected))]
    private static class Testing5
    {
        private static void Postfix(Panel_Milling __instance, ref GearItem __result)
        {
            if (__result == null) return;
            RarityUIManager.InstantiateOrMoveUILabel(__instance.m_NameLabel.gameObject.transform, 0, 35, 0);
            RarityUIManager.UpdateRarityTextAndColour(RarityManager.GetRarity(__result.name));
        }
    }
    
    // Works, however there is a bug where the last shown rarity stays - until it's updated by a new item.
    // Need to fix this by determining if there is an item being hovered over, if not then disable the label.
    [HarmonyPatch(typeof(Panel_ActionsRadial), nameof(Panel_ActionsRadial.GetActionText))]
    private static class Testing6
    {
        private static void Postfix(Panel_ActionsRadial __instance, RadialMenuArm arm)
        {
            if (arm.m_GearItem == null) return;
            RarityUIManager.InstantiateOrMoveUILabel(__instance.m_SegmentLabel.gameObject.transform, 0, 35, 0);
            RarityUIManager.UpdateRarityTextAndColour(RarityManager.GetRarity(arm.m_GearItem.name));
        }
    }
    
    // Need to add one more patch down here for when inspecting an item.
    // See code in the 'old' AdaptiveArsenal branch as I figured out how to include it with all the labels that fade in.
}