namespace ItemRarities;

internal sealed class Mod : MelonMod
{
    public override void OnSceneWasLoaded(int buildIndex, string sceneName)
    {
        if (ItemRarityManager.IsInitialized()) return;
        if (!GameManager.IsMainMenuActive()) return;
        
        MelonCoroutines.Start(ItemRarityManager.InitializeRarities());
    }
}