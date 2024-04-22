namespace Motorent.Domain.Common.ValueObjects;

public sealed class Name(string value) : ValueObject
{
    public string Value { get; private init; } = value;

    public override string ToString() => Value;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}