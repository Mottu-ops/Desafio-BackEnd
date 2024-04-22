using Motorent.Domain.Renters.Errors;
using Motorent.Domain.Renters.ValueObjects;
using Motorent.Domain.Users;

namespace Motorent.Domain.Renters.Services;

public sealed class RenterFactory(
    ICnpjUniquenessChecker cnpjUniquenessChecker,
    ICnhUniquenessChecker cnhUniquenessChecker)
    : IRenterFactory
{
    public async Task<Result<Renter>> CreateAsync(
        User user,
        RenterId renterId,
        Cnpj cnpj,
        Cnh cnh,
        CancellationToken cancellationToken = default)
    {
        if (!await cnpjUniquenessChecker.IsUniqueAsync(cnpj, cancellationToken))
        {
            return RenterErrors.DuplicateCnpj;
        }

        if (!await cnhUniquenessChecker.IsUniqueAsync(cnh, cancellationToken))
        {
            return RenterErrors.DuplicateCnh;
        }

        return new Renter(renterId, user.Id, cnpj, cnh, user.Name, user.Birthdate);
    }
}