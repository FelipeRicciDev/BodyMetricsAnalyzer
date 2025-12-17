namespace Domain.BodyComposition.Keys;

public static class CompositionSectionExtractor
{
    public static string Extract(string text)
    {
        var start = text.IndexOf("peso", StringComparison.OrdinalIgnoreCase);
        if (start == -1) return text;

        var end = text.IndexOf("análise de gordura", StringComparison.OrdinalIgnoreCase);
        return end > start ? text[start..end] : text[start..];
    }
}