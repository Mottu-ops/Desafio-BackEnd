using rent_core_api.Model;
using rent_core_api.RabbitMQ;
using rent_core_api.Repository;

namespace rent_core_api.Service
{
    public class MotoServiceImpl : MotoService
    {
        private readonly MotoRepository _motoRepository;
        private readonly RabbitMqPublisher _rabbitMqPublisher;
        private readonly LocacaoRepository _locacaoRepository;

        public MotoServiceImpl(MotoRepository motoRepository, RabbitMqPublisher rabbitMqPublisher, LocacaoRepository locacaoRepository)
        {
            this._motoRepository = motoRepository ?? throw new ArgumentNullException(nameof(motoRepository));
            this._rabbitMqPublisher = rabbitMqPublisher ?? throw new ArgumentNullException(nameof(rabbitMqPublisher));
            this._locacaoRepository = locacaoRepository ?? throw new ArgumentNullException(nameof(LocacaoRepository));
        }

        public void Add(Moto moto)
        {
            _motoRepository.Add(moto);
            _rabbitMqPublisher.PublishMotoEvent(moto);
        }

        public void Delete(int idMoto)
        {
            var moto = _motoRepository.GetById(idMoto);

            if (moto == null)
                throw new Exception("Nenhuma moto encontrada.");

            if (_locacaoRepository.ExisteLocacaoParaMoto(idMoto))
                throw new Exception("A moto não pode ser removida pois está associada a locações.");

            _motoRepository.Delete(moto);
        }

        public List<Moto> GetAll()
        {
            return _motoRepository.GetAll();
        }

        public Moto GetById(int id)
        {
            return _motoRepository.GetById(id);
        }

        public Moto GetByPlaca(string placa)
        {
            if (placa == null){
                throw new ArgumentNullException(nameof(placa), "A placa não pode ser nula.");
            }

            return _motoRepository.GetByPlaca(placa);
        }

        public void Update(Moto moto)
        {
            _motoRepository.Update(moto);
        }
    }
}
