using Job.Domain.Entities.Rental;

namespace Job.Domain.Repositories;

public interface IRentalRepository
{
    Task CreateAsync(RentalEntity rental, CancellationToken cancellationToken);

    Task<RentalEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<RentalEntity?> GetByMotoIdAsync(Guid id, CancellationToken cancellationToken);

    Task UpdateAsync(RentalEntity rental, CancellationToken cancellationToken);
}