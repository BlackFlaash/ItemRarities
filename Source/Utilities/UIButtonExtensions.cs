using Il2CppInterop.Runtime.InteropTypes.Arrays;

namespace ItemRarities.Utilities;

internal static class UIButtonExtensions
{
    private static UIButton Instantiate(this UIButton original)
    {
        var copy = GameObject.Instantiate(original.gameObject).GetComponent<UIButton>();
        copy.transform.parent = original.transform.parent;
        copy.transform.localPosition = original.transform.localPosition;
        copy.transform.localScale = Vector3.one;
        return copy;
    }
    
    internal static void NewSortButton(Il2CppReferenceArray<UIButton> uiButtonsArray, string buttonName, string spriteName, Action<UIButton> uiButtonMethod, float posX = 0f, float posY = 0f, float posZ = 0f)
    {
        UIButton temporaryUIButton = new();
            
        foreach (var button in uiButtonsArray)
        {
            temporaryUIButton = button;
        }
            
        var newButton = temporaryUIButton.Instantiate();
        newButton.name = buttonName;
        newButton.SetSpriteName(spriteName);
        EventDelegate.Set(newButton.onClick, new Action(() => uiButtonMethod(newButton)));
        newButton.transform.localPosition = new Vector3(posX, posY, posZ);
            
        uiButtonsArray.AddItem(newButton);
    }
    
    private static void SetSpriteName(this UIButton btn, string newSpriteName)
    {
        var sprite = btn.GetComponent<UISprite>();
        sprite.spriteName = newSpriteName;
        sprite.OnInit();
    }
}