namespace Domain.BodyComposition.DomainServices;

public static class BodyCompositionAnalyzer
{
    public static BodyCompositionExam Analyze(string rawText)
    {
        var normalized =
            CompositionTextNormalizer.NormalizeOcrText(rawText);

        var section =
            CompositionSectionExtractor.Extract(normalized);

        var composition =
            BodyCompositionParser.Parse(section);

        var header =
            BodyCompositionParser.ParseHeader(normalized);

        var score =
            BodyCompositionParser.ParseBodyScore(normalized);

        return new BodyCompositionExam(
            Header: header,
            Score: score,
            Composition: composition
        );
    }
}