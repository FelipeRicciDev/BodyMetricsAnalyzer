namespace Domain.BodyComposition.Parsers;

public static class BodyCompositionParser
{
    #region Body Composition Parsing
    public static BodyCompositionAnalysis Parse(string text)
    {
        var items = ParseMeasurements(text);
        ApplyPercentages(text, items);

        return new BodyCompositionAnalysis(
            Peso: Get(items, "peso"),
            MassaGorda: Get(items, "massa gorda"),
            MassaOssea: Get(items, "massa óssea"),
            MassaProteica: Get(items, "massa proteica"),
            AguaCorporal: Get(items, "água corporal"),
            MassaMuscular: Get(items, "massa muscular"),
            MusculoEsqueletico: Get(items, "músculo esquelético")
        );
    }

    public static Dictionary<string, BodyCompositionItem> ParseMeasurements(string text)
    {
        var items = new Dictionary<string, BodyCompositionItem>();

        foreach (Match match in BodyCompositionPatterns.Measurement.Matches(text))
        {
            var nome = match.Groups[1].Value;

            items[CompositionTextNormalizer.NormalizeKey(nome)] =
                new BodyCompositionItem
                {
                    Nome = nome,
                    MedicaoKg = match.Groups[2].Value,
                    Medidas = $"({match.Groups[3].Value})"
                };
        }

        return items;
    }

    public static void ApplyPercentages(
        string text,
        Dictionary<string, BodyCompositionItem> items)
    {
        var percentuais = BodyCompositionPatterns.Percentage.Matches(text)
            .Cast<Match>()
            .Select(m => new
            {
                Percentual = CompositionTextNormalizer.NormalizePercentual(m.Groups[1].Value),
                Avaliacao = CultureInfo.CurrentCulture.TextInfo
                    .ToTitleCase(m.Groups[2].Value)
            })
            .ToList();

        int index = 0;

        foreach (var item in items.Values)
        {
            if (index >= percentuais.Count) break;

            item.ProporcaoPercentual = percentuais[index].Percentual;
            item.Avaliacao = percentuais[index].Avaliacao;
            index++;
        }
    }

    public static BodyCompositionItem Get(
        Dictionary<string, BodyCompositionItem> items,
        string nome)
    {
        return items.TryGetValue(
            CompositionTextNormalizer.NormalizeKey(nome),
            out var item)
            ? item
            : new BodyCompositionItem
            {
                Nome = nome,
                Avaliacao = "Indisponível"
            };
    }

    #endregion

    #region Report Header Parsing
    public static BodyReportHeader ParseHeader(string text)
    {
        string id = BodyCompositionPatterns.ReportId
            .Match(text).Groups[1].Value;

        string sexo = BodyCompositionPatterns.Gender
            .Match(text).Groups[1].Value;

        int? idade = TryParseInt(text, BodyCompositionPatterns.Age);
        int? altura = TryParseInt(text, BodyCompositionPatterns.Height);

        DateTime? dataTeste =
            TryParseDateTime(text, BodyCompositionPatterns.TestDate);

        return new BodyReportHeader(
            Nome: string.IsNullOrWhiteSpace(id) ? null : id,
            Sexo: string.IsNullOrWhiteSpace(sexo) ? null : sexo,
            Idade: idade,
            AlturaCm: altura,
            DataExame: dataTeste
        );
    }

    public static int? TryParseInt(string text, Regex regex)
    {
        var m = regex.Match(text);
        return m.Success
            ? int.Parse(m.Groups[1].Value)
            : null;
    }

    public static DateTime? TryParseDateTime(string text, Regex regex)
    {
        var m = regex.Match(text);
        if (!m.Success) return null;

        return DateTime.TryParse(m.Groups[1].Value, out var dt)
            ? dt
            : null;
    }
    #endregion

    #region Body Score Parsing

    public static BodyScore ParseBodyScore(string text)
    {
        var match = BodyCompositionPatterns.BodyScore.Match(text);

        if (!match.Success) return new BodyScore(0, 100);

        return new BodyScore(
            Pontuacao: int.Parse(match.Groups[1].Value),
            Maximo: 100
        );
    }
    #endregion
}
