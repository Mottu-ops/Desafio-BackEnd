using rent_core_api.Model;

namespace rent_core_api.Service
{
    public interface LocacaoService
    {
        public Locacao CriarLocacao(int idMoto, int idEntregador, int idPlano);
        public Locacao FinalizarLocacao(int idLocacao, DateTime dataDevolucao);
    }
}
