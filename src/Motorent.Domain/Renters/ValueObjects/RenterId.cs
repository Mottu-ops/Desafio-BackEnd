using Motorent.Domain.Common.ValueObjects;

namespace Motorent.Domain.Renters.ValueObjects;

public sealed class RenterId(Ulid id) : EntityId<Ulid>(id)
{
    public static RenterId New() => new(Ulid.NewUlid());
}