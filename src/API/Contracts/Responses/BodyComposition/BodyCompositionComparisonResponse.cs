namespace API.Contracts.Responses.BodyComposition;

public sealed record BodyCompositionComparisonResponse
(
    UserResponse User,
    BodyScoreComparison Score,
    BodyCompositionComparison Comparison
);