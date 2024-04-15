namespace Motorent.Domain.Common.ValueObjects;

public abstract class EntityId<T>(T id) : ValueObject where T : notnull
{
    public T Value { get; } = id;

    public override string ToString() => Value.ToString()!;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}