using Microsoft.EntityFrameworkCore;
using rent_core_api.Infra;
using rent_core_api.Model;

namespace rent_core_api.Repository
{
    public class LocacaoRepositoryImpl : LocacaoRepository
    {
        private readonly ConnectionContext _connectionContext = new ConnectionContext();

        public void Add(Locacao locacao)
        {
            _connectionContext.Add(locacao);
            _connectionContext.SaveChanges();
        }

        public bool ExisteLocacaoParaMoto(long idMoto)
        {
            return _connectionContext.Locacoes.Any(l => l.IdMoto == idMoto);
        }

        public Locacao GetById(int id)
        {
            return _connectionContext.Locacoes.FirstOrDefault(l => l.Id == id);
        }

        public void Update(Locacao locacao)
        {
            _connectionContext.Update(locacao);
            _connectionContext.SaveChanges();
        }
    }
}
