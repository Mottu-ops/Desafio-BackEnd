using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Motorent.Application.Common.Abstractions.Identity;
using Motorent.Domain.Common.Entities;

namespace Motorent.Infrastructure.Common.Persistence.Interceptors;

internal sealed class AuditEntitiesOnSaveChangesInterceptor(TimeProvider timeProvider, IUserContext userContext)
    : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData data,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (data.Context is null)
        {
            return base.SavingChangesAsync(data, result, cancellationToken);
        }

        var auditableEntities = data.Context.ChangeTracker.Entries<IAuditable>()
            .ToArray();

        if (auditableEntities.Length is 0)
        {
            return base.SavingChangesAsync(data, result, cancellationToken);
        }

        var time = timeProvider.GetUtcNow();
        var user = userContext.IsAuthenticated
            ? userContext.UserId.ToString()
            : "unknown";

        foreach (var entry in auditableEntities)
        {
            if (entry.State is EntityState.Added)
            {
                entry.Entity.SetCreated(time, user);
            }

            if (entry.State is EntityState.Modified || HasChangedOwnedEntities(entry))
            {
                entry.Entity.SetUpdated(time, user);
            }
        }

        return base.SavingChangesAsync(data, result, cancellationToken);
    }

    private static bool HasChangedOwnedEntities(EntityEntry entry) =>
        entry.References.Any(r =>
            r.TargetEntry is { State: EntityState.Added or EntityState.Modified } &&
            r.TargetEntry.Metadata.IsOwned());
}