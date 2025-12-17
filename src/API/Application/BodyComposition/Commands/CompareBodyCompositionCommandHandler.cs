namespace API.Application.BodyComposition.Commands;

public sealed class CompareBodyCompositionCommandHandler(
    IPdfTextExtractor pdfTextExtractor) : IRequestHandler<
        CompareBodyCompositionCommand,
        BodyCompositionComparisonResponse>
{
    public async Task<BodyCompositionComparisonResponse> Handle(CompareBodyCompositionCommand request, CancellationToken cancellationToken)
    {
        CompareBodyCompositionCommandValidator.Validate(request);

        var pdfA = request.PdfFiles[0];
        var pdfB = request.PdfFiles[1];

        var textA = await pdfTextExtractor.ExtractTextAsync(
            pdfA.OpenReadStream(),
            cancellationToken);

        var textB = await pdfTextExtractor.ExtractTextAsync(
            pdfB.OpenReadStream(),
            cancellationToken);

        var examA = BodyCompositionAnalyzer.Analyze(textA);
        var examB = BodyCompositionAnalyzer.Analyze(textB);

        var olderExam =
            examA.Header.DataExame <= examB.Header.DataExame
                ? examA
                : examB;

        var newerExam =
            examA.Header.DataExame > examB.Header.DataExame
                ? examA
                : examB;

        var (compositionComparison, scoreComparison) =
            BodyCompositionComparator.Compare(olderExam, newerExam);

        return new BodyCompositionComparisonResponse(
            User: new UserResponse(
                Name: newerExam.Header.Nome!,
                Age: newerExam.Header.Idade!.Value,
                Sex: newerExam.Header.Sexo!,
                Height: newerExam.Header.AlturaCm!.Value
            ),
            Score: scoreComparison,
            Comparison: compositionComparison
        );
    }
}