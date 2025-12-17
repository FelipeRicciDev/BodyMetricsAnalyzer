namespace Domain.Records;

public record BodyCompositionAnalysis(
List<BodyMetric> Metrics,
double WeightTargetKg,
double WeightControlKg,
double FatControlKg,
double MuscleControlKg
);
