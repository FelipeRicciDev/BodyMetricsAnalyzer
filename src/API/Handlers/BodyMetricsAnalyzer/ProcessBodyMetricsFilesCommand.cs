namespace API.Handlers.BodyMetricsAnalyzer;

public sealed class ProcessBodyMetricsFilesCommand : IRequest
{
    public List<IFormFile> PdfFiles { get; init; } = [];

    public void Validate()
    {
        if (PdfFiles is null || PdfFiles.Count == 0)
            throw new ArgumentException("PDF não informado.");
    }
}