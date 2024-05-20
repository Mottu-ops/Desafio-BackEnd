using MotorcycleRental.Domain.Entities.Mongo;

namespace MotorcycleRental.Domain.Interfaces.Repositories.MongoDb
{
    public interface IMotorcycleMongoRepository
    {
        Task<IEnumerable<MotorcycleMgDb>> GetAllAsync();
        Task<MotorcycleMgDb> GetByIdAsync(Guid id);
        Task AddAsync(MotorcycleMgDb entity);
        Task UpdateAsync(MotorcycleMgDb entity);
    }
}
