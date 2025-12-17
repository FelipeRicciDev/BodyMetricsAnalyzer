namespace Domain.Records;

public class BodyCompositionItem
{
    public string Nome { get; init; } = "";
    public string MedicaoKg { get; init; } = "";
    public string Medidas { get; init; } = "";

    public string ProporcaoPercentual { get; set; } = "";
    public string Avaliacao { get; set; } = "";
}
