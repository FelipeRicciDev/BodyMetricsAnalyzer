namespace API.Application.BodyComposition.Commands;

public sealed class CompareBodyCompositionCommand
    : IRequest<BodyCompositionComparisonResponse>
{
    public List<IFormFile> PdfFiles { get; set; } = [];
}
