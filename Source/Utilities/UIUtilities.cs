namespace ItemRarities.Utilities;

internal static class UIUtilities
{
    internal static UILabel SetupGameObjectWithUILabel(string gameObjectName, Transform parent, bool worldPositionStays, float posX, float posY, float posZ)
    {
        GameObject gameObject = new(gameObjectName);
        var label = gameObject.AddComponent<UILabel>();
        gameObject.transform.SetParent(parent, worldPositionStays);
        gameObject.transform.localPosition = new Vector3(posX, posY, posZ);

        return label;
    }

    internal static void SetupUILabel(
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
        
        label.effectStyle = UILabel.Effect.Outline;
        label.effectColor = Color.black;
        label.effectDistance = new Vector2(0.5f, 0.5f);
    }
}