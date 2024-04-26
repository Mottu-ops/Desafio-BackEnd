using Job.Domain.Entities.Rent;

namespace Job.Domain.Repositories;

public interface IRentRepository
{
    Task CreateAsync(RentEntity rent, CancellationToken cancellationToken);

    Task<RentEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<RentEntity?> GetByMotoIdAsync(Guid id, CancellationToken cancellationToken);

    Task UpdateAsync(RentEntity rent, CancellationToken cancellationToken);
}