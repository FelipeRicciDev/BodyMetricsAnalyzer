namespace Domain.Records.Report;

public sealed record BodyCompositionItemComparison
(
    decimal OldValue,
    decimal NewValue,
    decimal Diference
);