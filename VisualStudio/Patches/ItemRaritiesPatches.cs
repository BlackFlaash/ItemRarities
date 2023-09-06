﻿using static ItemRarities.Main;

namespace ItemRarities
{
    [HarmonyPatch(typeof(ItemDescriptionPage), nameof(ItemDescriptionPage.UpdateGearItemDescription))]
    public static class ItemDescriptionPage_RarityLabelPatch
    {
        static UILabel? rarityLabel;
        static void Postfix(ItemDescriptionPage __instance)
        {
            if (__instance.m_ItemNameLabel == null) return;

            string displayedName = __instance.m_ItemNameLabel.text;
            Rarity itemRarity = gearRarities.ContainsKey(displayedName) ? gearRarities[displayedName] : Rarity.ERROR;
            Color rarityColor = GetColorForRarity(itemRarity);

            if (rarityLabel == null)
            {
                rarityLabel = UnityEngine.Object.Instantiate(__instance.m_ItemNameLabel);
                rarityLabel.transform.SetParent(__instance.m_ItemNameLabel.transform.parent, false);
                rarityLabel.transform.localPosition = new Vector3(__instance.m_ItemNameLabel.transform.localPosition.x,
                                                                  __instance.m_ItemNameLabel.transform.localPosition.y - -25,
                                                                  __instance.m_ItemNameLabel.transform.localPosition.z);
                rarityLabel.transform.localScale = new Vector3(0.75f, 0.75f, 1f);
            }

            rarityLabel.text = itemRarity.ToString();
            rarityLabel.color = rarityColor;
        }
    }

    [HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.UpdateLabels))]
    public static class PanelInventoryExamine_RarityLabelPatch
    {
        static UILabel? rarityLabel;

        static void Postfix(Panel_Inventory_Examine __instance)
        {
            if (__instance.m_Item_Label == null) return;

            string itemName = __instance.m_Item_Label.text;
            Rarity itemRarity = gearRarities.ContainsKey(itemName) ? gearRarities[itemName] : Rarity.ERROR;
            Color rarityColor = GetColorForRarity(itemRarity);

            if (rarityLabel == null)
            {
                rarityLabel = UnityEngine.Object.Instantiate(__instance.m_Item_Label);

                rarityLabel.transform.SetParent(__instance.m_Item_Label.transform.parent, false);
                rarityLabel.transform.localPosition = new Vector3(__instance.m_Item_Label.transform.localPosition.x,
                                                                  __instance.m_Item_Label.transform.localPosition.y - -25,
                                                                  __instance.m_Item_Label.transform.localPosition.z);

                rarityLabel.transform.localScale = new Vector3(0.75f, 0.75f, 1f);
            }

            rarityLabel.text = itemRarity.ToString();
            rarityLabel.color = rarityColor;
        }
    }

    [HarmonyPatch(typeof(Panel_Clothing), nameof(Panel_Clothing.RefreshVisuals))]
    public static class PanelClothing_RarityLabelPatch
    {
        static UILabel? clothingRarityLabel;

        static void Postfix(Panel_Clothing __instance)
        {
            if (__instance.m_ItemDescriptionPage == null || __instance.m_ItemDescriptionPage.m_ItemNameLabel == null) return;

            string itemName = __instance.m_ItemDescriptionPage.m_ItemNameLabel.text;
            Rarity itemRarity = gearRarities.ContainsKey(itemName) ? gearRarities[itemName] : Rarity.ERROR;
            Color rarityColor = GetColorForRarity(itemRarity);

            if (clothingRarityLabel == null)
            {
                clothingRarityLabel = UnityEngine.Object.Instantiate(__instance.m_ItemDescriptionPage.m_ItemNameLabel);

                clothingRarityLabel.transform.SetParent(__instance.m_ItemDescriptionPage.m_ItemNameLabel.transform.parent, false);
                clothingRarityLabel.transform.localPosition = new Vector3(__instance.m_ItemDescriptionPage.m_ItemNameLabel.transform.localPosition.x,
                                                                  __instance.m_ItemDescriptionPage.m_ItemNameLabel.transform.localPosition.y - -25,
                                                                  __instance.m_ItemDescriptionPage.m_ItemNameLabel.transform.localPosition.z);

                clothingRarityLabel.transform.localScale = new Vector3(0.75f, 0.75f, 1f);
            }

            clothingRarityLabel.text = itemRarity.ToString();
            clothingRarityLabel.color = rarityColor;
        }
    }

    [HarmonyPatch(typeof(Panel_Crafting), nameof(Panel_Crafting.Update))] // Need to find an alternative method. RefreshSelectedBlueprint is a possibly candidate but only calls when a method is calaced
    public static class PanelCrafting_RarityLabelPatch
    {
        static UILabel? rarityLabel;

        static void Postfix(Panel_Crafting __instance)
        {
            if (__instance.m_SelectedName == null) return;

            string itemName = __instance.m_SelectedName.text;
            Rarity itemRarity = gearRarities.ContainsKey(itemName) ? gearRarities[itemName] : Rarity.ERROR;
            Color rarityColor = GetColorForRarity(itemRarity);

            if (rarityLabel == null)
            {
                rarityLabel = UnityEngine.Object.Instantiate(__instance.m_SelectedName);

                rarityLabel.transform.SetParent(__instance.m_SelectedName.transform.parent, false);
                rarityLabel.transform.localPosition = new Vector3(__instance.m_SelectedName.transform.localPosition.x,
                                                                  __instance.m_SelectedName.transform.localPosition.y - -25,
                                                                  __instance.m_SelectedName.transform.localPosition.z);

                rarityLabel.transform.localScale = new Vector3(0.75f, 0.75f, 1f);
            }

            rarityLabel.text = itemRarity.ToString();
            rarityLabel.color = rarityColor;
        }
    }

    [HarmonyPatch(typeof(Panel_Cooking), nameof(Panel_Cooking.Update))] // Need to find an alternative method.
    public static class PanelCooking_RarityLabelPatch
    {
        static UILabel? rarityLabel;

        static void Postfix(Panel_Cooking __instance)
        {
            if (__instance.m_Label_CookedItemName == null) return;

            string itemName = __instance.m_Label_CookedItemName.text;
            Rarity itemRarity = gearRarities.ContainsKey(itemName) ? gearRarities[itemName] : Rarity.ERROR;
            Color rarityColor = GetColorForRarity(itemRarity);

            if (rarityLabel == null)
            {
                rarityLabel = UnityEngine.Object.Instantiate(__instance.m_Label_CookedItemName);

                rarityLabel.transform.SetParent(__instance.m_Label_CookedItemName.transform.parent, false);
                rarityLabel.transform.localPosition = new Vector3(__instance.m_Label_CookedItemName.transform.localPosition.x,
                                                                  __instance.m_Label_CookedItemName.transform.localPosition.y - -25,
                                                                  __instance.m_Label_CookedItemName.transform.localPosition.z);

                rarityLabel.transform.localScale = new Vector3(0.75f, 0.75f, 1f);
            }

            rarityLabel.text = itemRarity.ToString();
            rarityLabel.color = rarityColor;
        }
    }

    [HarmonyPatch(typeof(Panel_HUD), nameof(Panel_HUD.Update))] // Need to find an alternative method.
    public static class PanelHUD_RarityLabelPatch
    {
        static void Postfix(Panel_HUD __instance)
        {
            if (__instance.m_Label_ObjectName == null) return;

            string itemName = __instance.m_Label_ObjectName.text;
            Rarity itemRarity = gearRarities.ContainsKey(itemName) ? gearRarities[itemName] : Rarity.Default;
            Color rarityColor = GetColorForRarity(itemRarity);

            __instance.m_Label_ObjectName.color = rarityColor;
        }
    }

    [HarmonyPatch(typeof(PlayerManager), nameof(PlayerManager.InitLabelsForGear))]
    public static class PlayerManager_RarityLabelPatch
    {
        static UILabel? rarityLabel;

        static void Postfix(PlayerManager __instance)
        {
            Panel_HUD? actualHUDPanel = __instance.m_HUD.GetPanel();
            if (actualHUDPanel == null || actualHUDPanel.m_InspectMode_Title == null) return;

            string itemName = actualHUDPanel.m_InspectMode_Title.text;
            Rarity itemRarity = gearRarities.ContainsKey(itemName) ? gearRarities[itemName] : Rarity.ERROR;
            Color rarityColor = GetColorForRarity(itemRarity);

            if (rarityLabel == null)
            {
                rarityLabel = UnityEngine.Object.Instantiate(actualHUDPanel.m_InspectMode_Title);
                rarityLabel.alignment = NGUIText.Alignment.Center;  // Center the text

                rarityLabel.transform.SetParent(actualHUDPanel.m_InspectMode_Title.transform.parent, false);
                rarityLabel.transform.localPosition = new Vector3(actualHUDPanel.m_InspectMode_Title.transform.localPosition.x - 366,
                                                                  actualHUDPanel.m_InspectMode_Title.transform.localPosition.y - 290,
                                                                  actualHUDPanel.m_InspectMode_Title.transform.localPosition.z);

                rarityLabel.transform.localScale = new Vector3(0.75f, 0.75f, 1f);
            }

            rarityLabel.text = itemRarity.ToString();
            rarityLabel.color = rarityColor;
        }
    }

    [HarmonyPatch(typeof(Panel_ActionsRadial), nameof(Panel_ActionsRadial.UpdateDisplayText))]
    public static class PanelActionsRadial_RarityLabelPatch
    {
        static UILabel? rarityLabel;

        private static readonly HashSet<string> excludedNames = new HashSet<string>
            {
                "NAVIGATION",
                "CAMPCRAFT",
                "FIRST AID",
                "DRINK",
                "LIGHT SOURCES",
                "FOOD",
                "WEAPONS",
                "DROP DECOY",
                "OPEN MAP",
                "ROCK CACHE",
                "STATUS",
                "FIRE",
                "PASS TIME",
                "ICE FISHING HOLE",
                "SNOW SHELTER"
            };
        static void Postfix(Panel_ActionsRadial __instance)
        {
            if (__instance.m_SegmentLabel == null) return;

            string itemName = __instance.m_SegmentLabel.text;
            Rarity itemRarity = gearRarities.ContainsKey(itemName) ? gearRarities[itemName] : Rarity.ERROR;
            Color rarityColor = GetColorForRarity(itemRarity);

            if (excludedNames.Contains(itemName))
            {
                if (rarityLabel != null)
                {
                    rarityLabel.gameObject.SetActive(false);
                }
                return;
            }

            if (string.IsNullOrEmpty(itemName))
            {
                if (rarityLabel != null)
                {
                    rarityLabel.gameObject.SetActive(false);
                }
                return;
            }

            if (rarityLabel == null)
            {
                rarityLabel = UnityEngine.Object.Instantiate(__instance.m_SegmentLabel);

                rarityLabel.transform.SetParent(__instance.m_SegmentLabel.transform.parent, false);
                rarityLabel.transform.localPosition = new Vector3(__instance.m_SegmentLabel.transform.localPosition.x,
                                                                  __instance.m_SegmentLabel.transform.localPosition.y - -20,
                                                                  __instance.m_SegmentLabel.transform.localPosition.z);

                rarityLabel.transform.localScale = new Vector3(0.75f, 0.75f, 1f);
            }

            rarityLabel.text = itemRarity.ToString();
            rarityLabel.color = rarityColor;
            rarityLabel.gameObject.SetActive(true);
        }
    }

    [HarmonyPatch(typeof(Panel_Milling), nameof(Panel_Milling.Update))] // Need to find an alternative method.
    public static class PanelMilling_RarityLabelPatch
    {
        static UILabel? rarityLabel;

        static void Postfix(Panel_Milling __instance)
        {
            if (__instance.m_NameLabel == null) return;

            string itemName = __instance.m_NameLabel.text;
            Rarity itemRarity = gearRarities.ContainsKey(itemName) ? gearRarities[itemName] : Rarity.ERROR;
            Color rarityColor = GetColorForRarity(itemRarity);

            if (rarityLabel == null)
            {
                rarityLabel = UnityEngine.Object.Instantiate(__instance.m_NameLabel);

                rarityLabel.transform.SetParent(__instance.m_NameLabel.transform.parent, false);
                rarityLabel.transform.localPosition = new Vector3(__instance.m_NameLabel.transform.localPosition.x,
                                                                  __instance.m_NameLabel.transform.localPosition.y - -25,
                                                                  __instance.m_NameLabel.transform.localPosition.z);

                rarityLabel.transform.localScale = new Vector3(0.75f, 0.75f, 1f);
            }

            rarityLabel.text = itemRarity.ToString();
            rarityLabel.color = rarityColor;
        }
    }
}

// Possible Harmony Patch which can be used in the future?

/* [HarmonyPatch(typeof(Panel_GearSelect), nameof(Panel_GearSelect.Update))] // Need to find an alternative method. Slightly broken, all labels disapear after No Tools is selected
        public static class Panel_GearSelectAddRarityLabelPatch
        {
            static UILabel? rarityLabel;

            private static readonly HashSet<string> excludedNames = new HashSet<string>
            {
                "NO TOOL",
            };
            static void Postfix(Panel_GearSelect __instance)
            {
                if (__instance.m_Label == null) return;

                string itemName = __instance.m_Label.text;
                Rarity itemRarity = gearRarities.ContainsKey(itemName) ? gearRarities[itemName] : Rarity.ERROR;
                Color rarityColor = GetColorForRarity(itemRarity);

                if (excludedNames.Contains(itemName))
                {
                    if (rarityLabel != null)
                    {
                        rarityLabel.gameObject.SetActive(false);
                    }
                    return;
                }

                if (string.IsNullOrEmpty(itemName))
                {
                    if (rarityLabel != null)
                    {
                        rarityLabel.gameObject.SetActive(false);
                    }
                    return;
                }

                if (rarityLabel == null)
                {
                    rarityLabel = UnityEngine.Object.Instantiate(__instance.m_Label);

                    rarityLabel.transform.SetParent(__instance.m_Label.transform.parent, false);
                    rarityLabel.transform.localPosition = new Vector3(__instance.m_Label.transform.localPosition.x,
                                                                      __instance.m_Label.transform.localPosition.y - -15,
                                                                      __instance.m_Label.transform.localPosition.z);

                    rarityLabel.transform.localScale = new Vector3(0.75f, 0.75f, 1f);
                }

                rarityLabel.text = itemRarity.ToString();
                rarityLabel.color = rarityColor;
            }
        } */