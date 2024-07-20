using ItemRarities.Managers;
using ItemRarities.Utilities;

namespace ItemRarities.Patches;

internal static class ContainerPatches
{
    [HarmonyPatch(typeof(InventoryGridItem), nameof(InventoryGridItem.OnClick))]
    private static class InventoryGridItemClickPatch
    {
        private static void Postfix(InventoryGridItem __instance)
        {
            RarityUIManager.UpdateContainerColours(InterfaceManager.GetPanel<Panel_Container>());
        }
    }
    
    [HarmonyPatch(typeof(Panel_Container), nameof(Panel_Container.Initialize))]
    private static class PanelContainerInitializePatch
    {
        private static void Postfix(Panel_Container __instance)
        {
            UIButtonExtensions.NewSortButton(__instance.m_SortButtons, "Button_SortRarity", "ico_Star", __instance.OnSortInventoryChange, 100, 1.8f);
            __instance.m_SortFlipDictionary.Add("GAMEPLAY_SortRarity", false);
        }
    }
    
    [HarmonyPatch(typeof(Panel_Container), nameof(Panel_Container.OnSortInventoryChange))]
    private static class PanelContainerInventorySortedPatch
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
    private static class PanelContainerContainerListPatch
    {
        private static void Postfix(Panel_Container __instance)
        {
            if (__instance.m_InventorySortName == "GAMEPLAY_SortRarity")
            {
                RarityUIManager.CompareGearByRarity(__instance);
            }
        }
    }
    
    [HarmonyPatch(typeof(Panel_Container), nameof(Panel_Container.UpdateFilteredInventoryList))]
    private static class PanelContainerInventoryListPatch
    {
        private static void Postfix(Panel_Container __instance)
        {
            if (__instance.m_InventorySortName == "GAMEPLAY_SortRarity")
            {
                RarityUIManager.CompareGearByRarityInventory(__instance);
            }
        }
    }
    
    [HarmonyPatch(typeof(Panel_Container), nameof(Panel_Container.Update))]
    private static class PanelContainerUpdatePatch
    {
        private static void Postfix(Panel_Container __instance)
        {
            if (__instance.m_FilteredContainerList.Count == 0) return;
            RarityUIManager.UpdateContainerColours(__instance);
        }
    }
}