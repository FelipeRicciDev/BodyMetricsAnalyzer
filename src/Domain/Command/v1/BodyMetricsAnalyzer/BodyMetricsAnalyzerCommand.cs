namespace Domain.Command.v1.BodyMetricsAnalyzer;

public sealed record AnalyzeBodyMetricsCommand(AnaliseComposicaoCorporal Analise) : IRequest<AnaliseComposicaoCorporal>;