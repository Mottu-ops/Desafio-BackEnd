using Motorent.Domain.Common.ValueObjects;

namespace Motorent.Domain.Renters.ValueObjects;

public sealed class RenterId(Guid id) : EntityId<Guid>(id)
{
    public static RenterId New() => new(Guid.NewGuid());
}