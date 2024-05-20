using MotorcycleRental.Domain.Entities;

namespace MotorcycleRental.Domain.Interfaces
{
    public interface IDeliverymanRepository : IBaseRepository<Deliveryman>
    {
        Task<Deliveryman> GetByCnpjAsync(string cnpj);
        Task<Deliveryman> GetByCnhAsync(string cnh);
    }
}
