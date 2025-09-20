using System.Text.Json;
using CandidatosModel;
using CandidatosData;

namespace CandidatosBusiness;

public class ApostaSiteService(ApplicationDbContext context) : IApostaSiteService
{
    private readonly ApplicationDbContext _context = context;

    // CRUD
    public List<ApostaSiteModel> ListarTodos() => [.. _context.ApostaSites];

    public ApostaSiteModel? ObterPorId(string id) =>
        _context.ApostaSites.FirstOrDefault(s => s.Id == id);

    public ApostaSiteModel Criar(ApostaSiteModel site)
    {
        _context.ApostaSites.Add(site);
        _context.SaveChanges();
        RegistrarLog($"CRIAR: {site.Nome} ({site.Url})");
        return site;
    }

    public bool Atualizar(ApostaSiteModel site)
    {
        var existente = _context.ApostaSites.Find(site.Id);
        if (existente == null) return false;

        existente.Nome = site.Nome;
        existente.Url = site.Url;
        existente.Categoria = site.Categoria;
        existente.NivelRisco = site.NivelRisco;
        existente.DataCadastro = site.DataCadastro;

        _context.SaveChanges();
        RegistrarLog($"ATUALIZAR: {site.Id} -> {site.Nome} ({site.Url})");
        return true;
    }

    public bool Remover(string id)
    {
        var site = _context.ApostaSites.Find(id);
        if (site == null) return false;

        _context.ApostaSites.Remove(site);
        _context.SaveChanges();
        RegistrarLog($"REMOVER: {id}");
        return true;
    }

    // Arquivos
    public void ExportarJson(string caminhoArquivo)
    {
        var dados = ListarTodos();
        var json = JsonSerializer.Serialize(dados, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        File.WriteAllText(caminhoArquivo, json);
        RegistrarLog($"EXPORTAR_JSON: {caminhoArquivo} ({dados.Count} itens)");
    }

    public void RegistrarLog(string mensagem)
    {
        var baseDir = AppContext.BaseDirectory;
        var logPath = Path.Combine(baseDir, "logs.txt");
        var linha = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {mensagem}";
        File.AppendAllText(logPath, linha + Environment.NewLine);
    }
}
