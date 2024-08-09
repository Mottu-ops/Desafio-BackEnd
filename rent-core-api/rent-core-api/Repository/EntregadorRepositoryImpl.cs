using rent_core_api.Infra;
using rent_core_api.Model;

namespace rent_core_api.Repository
{
    public class EntregadorRepositoryImpl : EntregadorRepository
    {
        private readonly ConnectionContext _connectionContext = new ConnectionContext();

        public void Add(Entregador entregador)
        {
            _connectionContext.Add(entregador);
            _connectionContext.SaveChanges();
        }

        public Entregador GetById(int id)
        {
            return _connectionContext.Entregadores.FirstOrDefault(m => m.id == id);
        }

        public void Update(Entregador entregador)
        {
            _connectionContext.Update(entregador);
            _connectionContext.SaveChanges();
        }
    }
}
