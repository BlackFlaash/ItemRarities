namespace ItemRarities;

internal static class InitializeComponents
{
    [HarmonyPatch(typeof(GearItem), nameof(GearItem.Awake))]
    private static class BreathVisibility
    {
        private static void Postfix(GearItem __instance)
        {
            if (__instance.gameObject.GetComponent<ItemRarity>() == null)
            {
                __instance.gameObject.AddComponent<ItemRarity>();
            }
        }
    }
}