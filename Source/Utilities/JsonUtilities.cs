namespace ItemRarities.Utilities;

internal static class JsonUtilities
{
    internal static void ReadJSON(string jsonFilePath, out string jsonText)
    {
        using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(jsonFilePath);
        using StreamReader reader = new(stream);
        
        jsonText = reader.ReadToEnd();
    }
}