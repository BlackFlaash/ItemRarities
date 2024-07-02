namespace ItemRarities;

internal sealed class Mod : MelonMod
{
    public override void OnSceneWasLoaded(int buildIndex, string sceneName)
    {
        if (RarityManager.IsInitialized()) return;
        if (!GameManager.IsMainMenuActive()) return;
        
        MelonCoroutines.Start(RarityManager.InitializeRarities());
    }
}