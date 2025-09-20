using CandidatosModel;

namespace CandidatosBusiness
{
    public interface IApostaSiteService
    {
        // CRUD básico
        List<ApostaSiteModel> ListarTodos();
        ApostaSiteModel? ObterPorId(string id);
        ApostaSiteModel Criar(ApostaSiteModel site);
        bool Atualizar(ApostaSiteModel site);
        bool Remover(string id);

        // Arquivos (requisito do trabalho)
        void ExportarJson(string caminhoArquivo);
        void RegistrarLog(string mensagem);
    }
}
