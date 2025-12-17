namespace Domain.Records.Report;

public sealed record BodyCompositionComparison
(
    BodyCompositionItemComparison Weight,
    BodyCompositionItemComparison FatMass,
    BodyCompositionItemComparison BoneMass,
    BodyCompositionItemComparison ProteinMass,
    BodyCompositionItemComparison TotalBodyWater,
    BodyCompositionItemComparison MuscleMass,
    BodyCompositionItemComparison SkeletalMuscleMass,
    string EvaluationPeriod
);