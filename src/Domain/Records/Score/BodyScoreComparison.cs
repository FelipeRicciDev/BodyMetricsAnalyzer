namespace Domain.Records.Score;

public sealed record BodyScoreComparison
(
    int OldScore,
    int NewScore,
    int Difference
);