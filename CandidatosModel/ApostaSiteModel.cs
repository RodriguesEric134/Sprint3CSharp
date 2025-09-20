namespace CandidatosModel;

public class ApostaSiteModel
{
    public string? Id { get; set; }
    public required string Nome { get; set; }          // Nome do site
    public required string Url { get; set; }           // Endereço do site
    public string Categoria { get; set; } = "Geral";   // Ex.: Esportes, Cassino, Poker
    public string NivelRisco { get; set; } = "Médio";  // Baixo / Médio / Alto
    public DateTime DataCadastro { get; set; }         // Quando foi adicionado

    public ApostaSiteModel()
    {
        Id = Guid.NewGuid().ToString();
        DataCadastro = DateTime.UtcNow;
    }
}
