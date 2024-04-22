using Microsoft.EntityFrameworkCore;
using Motorent.Domain.Renters;
using Motorent.Domain.Renters.Services;
using Motorent.Domain.Renters.ValueObjects;
using Motorent.Infrastructure.Common.Persistence;

namespace Motorent.Infrastructure.Renters;

internal sealed class CnpjUniquenessChecker(DataContext dataContext) : ICnpjUniquenessChecker
{
    public async Task<bool> IsUniqueAsync(Cnpj cnpj, CancellationToken cancellationToken = default) =>
        !await dataContext.Set<Renter>()
            .AnyAsync(r => r.Cnpj == cnpj, cancellationToken);
}