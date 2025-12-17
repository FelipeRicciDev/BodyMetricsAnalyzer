namespace API.Application.BodyComposition.Commands;

public sealed class CompareBodyCompositionCommandValidator
{
    public static void Validate(CompareBodyCompositionCommand command)
    {
        if (command.PdfFiles is null || command.PdfFiles.Count == 0)
            throw new ArgumentException("PDF não informado.");
    }
}