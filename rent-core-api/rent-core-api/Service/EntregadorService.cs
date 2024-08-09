using rent_core_api.Model;

namespace rent_core_api.Service
{
    public interface EntregadorService
    {
        void Add(Entregador entregador);
        Entregador GetById(int id);
        void Update(Entregador entregador);
    }
}
