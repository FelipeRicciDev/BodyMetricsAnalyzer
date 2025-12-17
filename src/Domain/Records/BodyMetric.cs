namespace Domain.Records;

public record BodyMetric(
    string Name,
    double MeasurementKg,
    string StandardRange,
    double WeightProportionPercent,
    string Evaluation
);
