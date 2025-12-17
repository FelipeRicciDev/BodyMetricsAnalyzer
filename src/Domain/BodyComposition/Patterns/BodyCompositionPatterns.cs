namespace Domain.BodyComposition.Patterns;

public static class BodyCompositionPatterns
{
    #region Measurements
    public static readonly Regex Measurement =
        new(
            @"(peso|massa gorda|massa óssea|massa proteica|água corporal|massa muscular|músculo esquelético)\s*" +
            @"([\d]+(?:\.\d+)?)\s*\(([\d\.\-]+)\)",
            RegexOptions.IgnoreCase | RegexOptions.Compiled
        );

    public static readonly Regex Percentage =
        new(
            @"([\d]+(?:\.\d+)?)\s*(alto|normal|baixo|excelente)",
            RegexOptions.IgnoreCase | RegexOptions.Compiled
        );

    #endregion

    #region Header
    public static readonly Regex ReportId =
        new(@"id\s*:\s*(\w+)",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

    public static readonly Regex Gender =
        new(@"sexo\s*:\s*(masculino|feminino)",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

    public static readonly Regex Age =
        new(@"idade\s*:\s*(\d+)",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

    public static readonly Regex Height =
        new(@"altura\s*:\s*(\d+)\s*cm",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

    public static readonly Regex TestDate =
        new(@"tempo\s*de\s*teste\s*:\s*(\d{2}/\d{2}/\d{4}\s*\d{2}:\d{2}:\d{2})",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

    #endregion

    #region Score
    public static readonly Regex BodyScore =
        new(
            @"pontua[cç][aã]o\s+corporal[\s\S]{0,40}?\b(\d{1,3})\b[\s\S]{0,40}?(?:\/\s*100|pontos)",
            RegexOptions.IgnoreCase | RegexOptions.Compiled
        );

    #endregion
}