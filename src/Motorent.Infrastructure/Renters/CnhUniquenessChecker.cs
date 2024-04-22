using Microsoft.EntityFrameworkCore;
using Motorent.Domain.Renters;
using Motorent.Domain.Renters.Services;
using Motorent.Domain.Renters.ValueObjects;
using Motorent.Infrastructure.Common.Persistence;

namespace Motorent.Infrastructure.Renters;

internal sealed class CnhUniquenessChecker(DataContext dataContext) : ICnhUniquenessChecker
{
    public async Task<bool> IsUniqueAsync(Cnh cnh, CancellationToken cancellationToken = default) =>
        !await dataContext.Set<Renter>()
            .AnyAsync(r => r.Cnh.Number == cnh.Number, cancellationToken);
}