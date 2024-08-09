using rent_core_api.Infra;
using rent_core_api.Model;

namespace rent_core_api.Repository
{
    public class PlanoRepositoryImpl : PlanoRepository
    {
        private readonly ConnectionContext _connectionContext = new ConnectionContext();

        public Plano GetById(int id)
        {
            return _connectionContext.Planos.FirstOrDefault(p => p.Id == id);
        }
    }
}
