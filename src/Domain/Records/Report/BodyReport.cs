namespace Domain.Records.Report;

public record BodyReport(
    BodyReportHeader Header,
    BodyScore Score,
    BodyCompositionAnalysis Body
);