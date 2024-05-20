using MotorcycleRental.Domain.Entities;

namespace MotorcycleRental.Domain.Interfaces
{
    public interface IMotorcycleRepository : IBaseRepository<Motorcycle>
    {
        Task<Motorcycle> GetByPlateAsync(string Plate);
    }
}