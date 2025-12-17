namespace API.Services.Pdf;

public sealed class PdfTextOcrExtractor(
    PdfImageExtractor pdfImageExtractor,
    OcrService ocrService) : IPdfTextExtractor
{
    private readonly PdfImageExtractor _pdfImageExtractor = pdfImageExtractor;
    private readonly OcrService _ocrService = ocrService;

    public async Task<string> ExtractTextAsync(
        Stream pdfStream,
        CancellationToken cancellationToken = default)
    {
        var images = _pdfImageExtractor.ExtractImages(pdfStream);

        var textBuilder = new StringBuilder();

        foreach (var image in images)
        {
            var text = await _ocrService.ReadTextAsync(image, cancellationToken);
            textBuilder.AppendLine(text);
        }

        return textBuilder.ToString();
    }
}
