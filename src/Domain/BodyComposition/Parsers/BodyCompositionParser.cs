using Domain.BodyComposition.Normalizers;
using Domain.Records;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Domain.BodyComposition.Parsers;

public static class BodyCompositionParser
{
    private static readonly Regex MedicaoRegex = new(
        @"(peso|massa gorda|massa óssea|massa proteica|água corporal|massa muscular|músculo esquelético)\s*" +
        @"([\d]+(?:\.\d+)?)\s*\(([\d\.\-]+)\)",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);

    private static readonly Regex PercentualRegex = new(
        @"([\d]+(?:\.\d+)?)\s*(alto|normal|baixo|excelente)",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);

    public static AnaliseComposicaoCorporal Parse(string text)
    {
        var items = ParseMedicoes(text);
        ApplyPercentuais(text, items);

        return new AnaliseComposicaoCorporal
        {
            Peso = Get(items, "Peso"),
            MassaGorda = Get(items, "Massa Gorda"),
            MassaOssea = Get(items, "Massa Óssea"),
            MassaProteica = Get(items, "Massa Proteica"),
            AguaCorporal = Get(items, "Água Corporal"),
            MassaMuscular = Get(items, "Massa Muscular"),
            MusculoEsqueletico = Get(items, "Músculo Esquelético")
        };
    }

    private static Dictionary<string, BodyCompositionItem> ParseMedicoes(string text)
    {
        var items = new Dictionary<string, BodyCompositionItem>();

        foreach (Match match in MedicaoRegex.Matches(text))
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

    private static void ApplyPercentuais(
        string text,
        Dictionary<string, BodyCompositionItem> items)
    {
        var percentuais = PercentualRegex.Matches(text)
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

    private static BodyCompositionItem Get(
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
}