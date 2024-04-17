using Motorent.Domain.Common.ValueObjects;

namespace Motorent.Domain.Users.ValueObjects;

public sealed class UserId(Guid id) : EntityId<Guid>(id)
{
    public static UserId New() => new(Guid.NewGuid());
}