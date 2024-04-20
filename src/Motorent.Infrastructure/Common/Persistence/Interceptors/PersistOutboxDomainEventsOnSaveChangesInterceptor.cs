using Microsoft.EntityFrameworkCore.Diagnostics;
using Motorent.Domain.Common.Entities;
using Motorent.Infrastructure.Common.Outbox;

namespace Motorent.Infrastructure.Common.Persistence.Interceptors;

internal sealed class PersistOutboxDomainEventsOnSaveChangesInterceptor : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData data,
        InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        if (data.Context is null)
        {
            return await base.SavingChangesAsync(data, result, cancellationToken);
        }

        var entities = data.Context.ChangeTracker
            .Entries<IEntity>()
            .Where(e => e.Entity.Events.Count is not 0)
            .Select(e => e.Entity)
            .ToList();

        var messages = entities
            .SelectMany(e => e.Events)
            .Select(OutboxMessage.Create)
            .ToArray();

        await data.Context.Set<OutboxMessage>()
            .AddRangeAsync(messages, cancellationToken);

        entities.ForEach(e => e.ClearEvents());

        return await base.SavingChangesAsync(data, result, cancellationToken);
    }
}