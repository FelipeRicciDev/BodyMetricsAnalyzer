namespace Domain.Records
{
    public class AnaliseComposicaoCorporal
    {
        public BodyCompositionItem Peso { get; set; } = default!;
        public BodyCompositionItem MassaGorda { get; set; } = default!;
        public BodyCompositionItem MassaOssea { get; set; } = default!;
        public BodyCompositionItem MassaProteica { get; set; } = default!;
        public BodyCompositionItem AguaCorporal { get; set; } = default!;
        public BodyCompositionItem MassaMuscular { get; set; } = default!;
        public BodyCompositionItem MusculoEsqueletico { get; set; } = default!;
    }
}
