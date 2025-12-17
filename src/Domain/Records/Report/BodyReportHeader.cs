namespace Domain.Records.Report;

public sealed record BodyReportHeader
(
    string Nome,
    string Sexo,
    int? Idade,
    int? AlturaCm,
    DateTime? DataExame
);