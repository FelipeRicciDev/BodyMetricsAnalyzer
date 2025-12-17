namespace API.Services.Ocr;

public sealed class OcrService
{
    private const string DefaultLanguage = "por";

    private readonly string _tessdataPath =
        Path.Combine(AppContext.BaseDirectory, "tessdata");

    public Task<string> ReadTextAsync(Bitmap image, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        using var engine = new TesseractEngine(
            _tessdataPath,
            DefaultLanguage,
            EngineMode.LstmOnly);

        engine.DefaultPageSegMode = PageSegMode.SingleColumn;

        using var pix = PixConverter.ToPix(image);
        using var page = engine.Process(pix);

        return Task.FromResult(page.GetText());
    }
}