namespace Domain.Records.Composition;

public sealed record BodyCompositionAnalysis
(
    BodyCompositionItem Peso,
    BodyCompositionItem MassaGorda,
    BodyCompositionItem MassaOssea,
    BodyCompositionItem MassaProteica,
    BodyCompositionItem AguaCorporal,
    BodyCompositionItem MassaMuscular,
    BodyCompositionItem MusculoEsqueletico
);
