using Microsoft.EntityFrameworkCore;
using Motorent.Domain.Users;
using Motorent.Domain.Users.Services;
using Motorent.Infrastructure.Common.Persistence;

namespace Motorent.Infrastructure.Users;

internal sealed class EmailUniquenessChecker(DataContext dataContext) : IEmailUniquenessChecker
{
    public async Task<bool> IsUniqueAsync(string email, CancellationToken cancellationToken = default) =>
        !await dataContext.Set<User>()
            .AnyAsync(u => u.Email.ToLower() == email.ToLower(), cancellationToken);
}