using rent_core_api.Model;

namespace rent_core_api.Service
{
    public interface MotoService
    {
        void Add(Moto moto);
        void Update(Moto moto);
        void Delete(int idMoto);
        List<Moto> GetAll();
        Moto GetByPlaca(string placa);
        Moto GetById(int id);

    }
}
