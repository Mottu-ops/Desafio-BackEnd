using Job.Domain.Entities.User;

namespace Job.Domain.Repositories;

public interface IMotoboyRepository
{
    Task<MotoboyEntity?> GetAsync(string cnpj, string password, CancellationToken cancellationToken);
    Task<MotoboyEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<MotoboyEntity?> GetByCnpjAsync(string cnpj, CancellationToken cancellationToken);
    Task<bool> CheckCnpjExistsAsync(string cnpj, CancellationToken cancellationToken);
    Task CreateAsync(MotoboyEntity motoboy, CancellationToken cancellationToken);
    Task UpdateAsync(MotoboyEntity motoboy, CancellationToken cancellationToken);
}