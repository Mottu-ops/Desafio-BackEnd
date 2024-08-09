using rent_core_api.Infra;
using rent_core_api.Model;

namespace rent_core_api.Repository
{
    public class MotoRepositoryImpl : MotoRepository
    {
        private readonly ConnectionContext _connectionContext = new ConnectionContext();
        public void Add(Moto moto)
        {
            _connectionContext.Add(moto);
            _connectionContext.SaveChanges();
        }

        public void AddCurrentYear(MotoEvents motoEvents)
        {
            _connectionContext.Add(motoEvents);
            _connectionContext.SaveChanges();
        }

        public void Delete(Moto moto)
        {
            _connectionContext.Remove(moto);
            _connectionContext.SaveChanges();
        }

        public List<Moto> GetAll()
        {
            return _connectionContext.Motos.ToList();
        }

        public Moto GetById(int id)
        {
            return _connectionContext.Motos.FirstOrDefault(m => m.id == id);
        }

        public Moto GetByPlaca(string placa)
        {
            return _connectionContext.Motos.FirstOrDefault(m => m.placa == placa)!;
        }

        public void Update(Moto moto)
        {
            _connectionContext.Update(moto);
            _connectionContext.SaveChanges();
        }
    }
}
