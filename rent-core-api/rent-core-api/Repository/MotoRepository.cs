using rent_core_api.Model;

namespace rent_core_api.Repository
{
    public interface MotoRepository
    {
        void Add(Moto moto);
        void Update(Moto moto);
        void Delete(Moto moto);
        List<Moto> GetAll();
        Moto GetByPlaca(string placa);
        Moto GetById(int id);
        void AddCurrentYear(MotoEvents motoEvents);
    }
}
