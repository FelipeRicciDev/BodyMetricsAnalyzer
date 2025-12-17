namespace API.Services;

public sealed class OcrService
{
    private readonly string _tessdataPath =
        Path.Combine(AppContext.BaseDirectory, "tessdata");

    public string ExtractText(Bitmap image)
    {
        using var engine = new TesseractEngine(
            _tessdataPath,
            "por",
            EngineMode.LstmOnly);

        engine.DefaultPageSegMode = PageSegMode.SingleColumn;

        using var pix = PixConverter.ToPix(image);
        using var page = engine.Process(pix);

        return page.GetText();
    }
}