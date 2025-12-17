namespace Domain.Records.Report;

public sealed record BodyCompositionExam
(
    BodyReportHeader Header,
    BodyScore Score,
    BodyCompositionAnalysis Composition
);