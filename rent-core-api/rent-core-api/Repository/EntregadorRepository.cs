using rent_core_api.Model;

namespace rent_core_api.Repository
{
    public interface EntregadorRepository
    {
        void Add(Entregador entregador);
        Entregador GetById(int id);
        void Update(Entregador entregador);

    }
}
