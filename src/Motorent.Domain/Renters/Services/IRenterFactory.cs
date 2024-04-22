using Motorent.Domain.Renters.ValueObjects;
using Motorent.Domain.Users;

namespace Motorent.Domain.Renters.Services;

public interface IRenterFactory
{
    Task<Result<Renter>> CreateAsync(
        User user,
        RenterId renterId,
        Cnpj cnpj,
        Cnh cnh,
        CancellationToken cancellationToken = default);
}