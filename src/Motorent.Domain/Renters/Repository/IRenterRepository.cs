using Motorent.Domain.Common.Repository;
using Motorent.Domain.Renters.ValueObjects;
using Motorent.Domain.Users.ValueObjects;

namespace Motorent.Domain.Renters.Repository;

public interface IRenterRepository : IRepository<Renter, RenterId>
{
    Task<Renter?> FindByUserAsync(UserId userId, CancellationToken cancellationToken = default);
    
    Task<bool> ExistsByUserAsync(UserId userId, CancellationToken cancellationToken = default);
}