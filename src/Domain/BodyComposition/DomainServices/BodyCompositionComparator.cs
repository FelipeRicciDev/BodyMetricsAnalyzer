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
                Diference: newer.Score.Pontuacao - older.Score.Pontuacao
            )
        );
    }

    private static BodyCompositionItemComparison CompareItem(
        BodyCompositionItem older,
        BodyCompositionItem newer)
    {
        var oldValue = decimal.Parse(older.MedicaoKg, CultureInfo.InvariantCulture);
        var newValue = decimal.Parse(newer.MedicaoKg, CultureInfo.InvariantCulture);

        return new BodyCompositionItemComparison(
            OldValue: oldValue,
            NewValue: newValue,
            Diference: newValue - oldValue
        );
    }
}