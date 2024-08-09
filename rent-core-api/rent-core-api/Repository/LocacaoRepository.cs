using rent_core_api.Model;

namespace rent_core_api.Repository
{
    public interface LocacaoRepository
    {
        void Add(Locacao locacao);
        Locacao GetById(int id);
        void Update(Locacao locacao);
        bool ExisteLocacaoParaMoto(long idMoto);
    }
}
