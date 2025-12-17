namespace Domain.Command.v1.BodyMetricsAnalyzer;

public sealed class AnalyzeBodyMetricsCommandHandler : IRequestHandler<AnalyzeBodyMetricsCommand, AnaliseComposicaoCorporal>
{
    public Task<AnaliseComposicaoCorporal> Handle(AnalyzeBodyMetricsCommand request, CancellationToken cancellationToken)
    {
        var t = request.Analise.Peso;
        return Task.FromResult(request.Analise);
    }
}