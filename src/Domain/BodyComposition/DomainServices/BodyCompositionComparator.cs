namespace Domain.BodyComposition.DomainServices;

public static class BodyCompositionComparator
{
    public static (
        BodyCompositionComparison Composition,
        BodyScoreComparison Score
    )
    Compare(BodyCompositionExam older, BodyCompositionExam newer)
    {
        return (
            new BodyCompositionComparison(
                Weight: CompareItem(older.Composition.Peso, newer.Composition.Peso),
                FatMass: CompareItem(older.Composition.MassaGorda, newer.Composition.MassaGorda),
                BoneMass: CompareItem(older.Composition.MassaOssea, newer.Composition.MassaOssea),
                ProteinMass: CompareItem(older.Composition.MassaProteica, newer.Composition.MassaProteica),
                TotalBodyWater: CompareItem(older.Composition.AguaCorporal, newer.Composition.AguaCorporal),
                MuscleMass: CompareItem(older.Composition.MassaMuscular, newer.Composition.MassaMuscular),
                SkeletalMuscleMass: CompareItem(older.Composition.MusculoEsqueletico, newer.Composition.MusculoEsqueletico),
                EvaluationPeriod: $"{older.Header.DataExame:MM/yyyy} → {newer.Header.DataExame:MM/yyyy}"
            ),
            new BodyScoreComparison(
                OldScore: older.Score.Pontuacao,
                NewScore: newer.Score.Pontuacao,
                Difference: newer.Score.Pontuacao - older.Score.Pontuacao
            )
        );
    }

    private static BodyCompositionItemComparison CompareItem(
        BodyCompositionItem older,
        BodyCompositionItem newer)
    {
        var oldValue = ParseDecimalSafe(older.MedicaoKg);
        var newValue = ParseDecimalSafe(newer.MedicaoKg);

        return new BodyCompositionItemComparison(
            OldValue: oldValue,
            NewValue: newValue,
            Difference: newValue - oldValue
        );
    }

    private static decimal ParseDecimalSafe(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return 0m;

        value = value
            .Replace("kg", "", StringComparison.OrdinalIgnoreCase)
            .Replace("%", "")
            .Replace(",", ".")
            .Trim();

        return decimal.TryParse(
            value,
            NumberStyles.Any,
            CultureInfo.InvariantCulture,
            out var result
        )
            ? result
            : 0m;
    }
}