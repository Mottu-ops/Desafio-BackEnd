using Motorent.Domain.Renters.ValueObjects;

namespace Motorent.Domain.Renters.Services;

public interface ICnhUniquenessChecker
{
    Task<bool> IsUniqueAsync(Cnh cnh, CancellationToken cancellationToken = default);
}