namespace Domain.BodyComposition.Sections;

public static class CompositionSectionExtractor
{
    #region Body Composition Section Extraction
    public static string Extract(string text)
    {
        var start = text.IndexOf("peso", StringComparison.OrdinalIgnoreCase);
        if (start == -1) return text;

        var end = text.IndexOf("análise de gordura", StringComparison.OrdinalIgnoreCase);
        return end > start ? text[start..end] : text[start..];
    }
    #endregion
}