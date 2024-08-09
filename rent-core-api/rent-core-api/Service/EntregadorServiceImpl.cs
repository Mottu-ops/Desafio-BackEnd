using rent_core_api.Model;
using rent_core_api.Repository;

namespace rent_core_api.Service
{
    public class EntregadorServiceImpl : EntregadorService
    {
        private readonly EntregadorRepository _entregadorRepository;

        public EntregadorServiceImpl(EntregadorRepository entregadorRepository)
        {
            this._entregadorRepository = entregadorRepository ?? throw new ArgumentNullException(nameof(entregadorRepository));
        }

        public void Add(Entregador entregador)
        {
            _entregadorRepository.Add(entregador);
        }

        public Entregador GetById(int id)
        {
            return _entregadorRepository.GetById(id);
        }

        public void Update(Entregador entregador)
        {
            _entregadorRepository.Update(entregador);
        }
    }
}
