using ItemRarities.Managers;

namespace ItemRarities.Patches;

internal static class ClothingPatches
{
    [HarmonyPatch(typeof(ClothingSlot), nameof(ClothingSlot.ActivateMouseHoverHighlight))]
    private static class ClothingSlotHoverPatch
    {
        private static void Postfix(ClothingSlot __instance)
        {
            RarityUIManager.UpdateClothingSlotColors(__instance);
        }
    }
    
    [HarmonyPatch(typeof(ClothingSlot), nameof(ClothingSlot.SetSelected))]
    private static class ClothingSlotSelectedPatch
    {
        private static void Postfix(ClothingSlot __instance, bool isSelected)
        {
            RarityUIManager.UpdateClothingSlotColors(__instance);
        }
    }
    
    [HarmonyPatch(typeof(Panel_Clothing), nameof(Panel_Clothing.Enable))]
    private static class ClothingSlotEnabledPatch
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
    private static class ClothingSlotUsePatch
    {
        private static void Postfix(Panel_Clothing __instance)
        {
            // Need this to properly update the colours for some reason?
            MelonCoroutines.Start(RarityUIManager.DelayedUpdateAllSlots(__instance));
        }
    }
}