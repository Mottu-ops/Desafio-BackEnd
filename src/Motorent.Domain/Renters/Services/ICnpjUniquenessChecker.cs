using Motorent.Domain.Renters.ValueObjects;

namespace Motorent.Domain.Renters.Services;

public interface ICnpjUniquenessChecker
{
    Task<bool> IsUniqueAsync(Cnpj cnpj, CancellationToken cancellationToken = default);
}