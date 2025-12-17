namespace API.Handlers.BodyMetricsAnalyzer;

public sealed class ProcessBodyMetricsFilesCommandHandler(
    PdfImageExtractor _pdfImageExtractor,
    OcrService _ocrService,
    IMediator _mediator) : IRequestHandler<ProcessBodyMetricsFilesCommand>
{
    public async Task Handle(ProcessBodyMetricsFilesCommand request, CancellationToken cancellationToken)
    {
        request.Validate();

        var images = _pdfImageExtractor.Extract(request.PdfFiles.First());
        var rawText = new StringBuilder();

        foreach (var image in images)
        {
            using var gray = ImagePreprocessor.ToGrayscale(image);
            rawText.AppendLine(_ocrService.ExtractText(gray));
        }

        var normalizedText =
            CompositionTextNormalizer.NormalizeOcrText(rawText.ToString());

        var compositionSection =
            CompositionSectionExtractor.Extract(normalizedText);

        var analise = BodyCompositionParser.Parse(compositionSection)
            ?? throw new Exception("Não foi possível extrair a análise de composição corporal.");

        _ = await _mediator.Send(
            new AnalyzeBodyMetricsCommand(analise),
            cancellationToken
        );
    }
}