namespace API.Application.BodyComposition.Commands;

public sealed record CompareBodyCompositionCommand
    : IRequest<BodyCompositionComparisonResponse>
{
    public List<IFormFile> PdfFiles { get; init; } = [];
}