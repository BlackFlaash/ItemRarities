namespace ItemRarities.Utilities;

internal static class UILabelExtensions
{
    internal static UILabel SetupGameObjectWithUILabel(string gameObjectName, Transform parent, bool worldPositionStays, bool inspectLabel = false, float posX = 0f, float posY = 0f, float posZ = 0f)
    {
        GameObject gameObject = new(gameObjectName);
        var uiLabel = gameObject.AddComponent<UILabel>();
        gameObject.transform.SetParent(parent, worldPositionStays);
        gameObject.transform.localPosition = new Vector3(posX, posY, posZ);

        SetupUILabel(
            uiLabel,
            string.Empty,
            FontStyle.Normal,
            UILabel.Crispness.Always,
            inspectLabel ? NGUIText.Alignment.Left : NGUIText.Alignment.Center,
            UILabel.Overflow.ResizeFreely,
            true,
            inspectLabel ? 2 : 0,
            inspectLabel ? 14 : 18,
            Color.clear,
            true
        );

        return uiLabel;
    }

    private static void SetupUILabel(
        UILabel label,
        string text,
        FontStyle fontStyle,
        UILabel.Crispness crispness,
        NGUIText.Alignment alignment,
        UILabel.Overflow overflow,
        bool multiLine,
        int depth,
        int fontSize,
        Color color,
        bool capsLock)
    {
        label.text = text;
        label.ambigiousFont = GameManager.GetFontManager().GetUIFontForCharacterSet(FontManager.m_CurrentCharacterSet);
        label.bitmapFont = GameManager.GetFontManager().GetUIFontForCharacterSet(FontManager.m_CurrentCharacterSet);
        label.font = GameManager.GetFontManager().GetUIFontForCharacterSet(FontManager.m_CurrentCharacterSet);

        label.fontStyle = fontStyle;
        label.keepCrispWhenShrunk = crispness;
        label.alignment = alignment;
        label.overflowMethod = overflow;
        label.multiLine = multiLine;
        label.depth = depth;
        label.fontSize = fontSize;
        label.color = color;
        label.capsLock = capsLock;
        
        /*label.effectStyle = UILabel.Effect.Outline;
        label.effectColor = Color.black;
        label.effectDistance = new Vector2(0.5f, 0.5f);*/
    }
}