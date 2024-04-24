using Motorent.Domain.Common.ValueObjects;

namespace Motorent.Domain.Users.ValueObjects;

public sealed class UserId(Ulid id) : EntityId<Ulid>(id)
{
    public static UserId New() => new(Ulid.NewUlid());
}