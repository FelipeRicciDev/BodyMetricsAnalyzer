namespace Domain.Records.Report;

public sealed record BodyCompositionReport
(
    BodyReportHeader Header,
    BodyScore Score,
    BodyCompositionAnalysis Composition
);