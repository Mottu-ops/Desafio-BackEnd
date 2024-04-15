namespace Motorent.Domain.Common.ValueObjects;

public abstract class ValueObject
{
    public static bool operator ==(ValueObject? left, ValueObject? right) => 
        left?.Equals(right) ?? ReferenceEquals(right, null);

    public static bool operator !=(ValueObject? left, ValueObject? right) => !(left == right);

    protected abstract IEnumerable<object?> GetEqualityComponents();

    public bool Equals(ValueObject? other)
    {
        return !ReferenceEquals(null, other) && (ReferenceEquals(this, other) 
                                                 || (GetType() == other.GetType() && GetEqualityComponents()
                                                     .SequenceEqual(other.GetEqualityComponents())));
    }

    public override bool Equals(object? obj)
    {
        return !ReferenceEquals(null, obj) && (ReferenceEquals(this, obj)
                                               || (obj.GetType() == GetType() && Equals((ValueObject)obj)));
    }

    public override int GetHashCode() => GetEqualityComponents()
        .Select(x => x?.GetHashCode() ?? 0)
        .Aggregate((current, next) => unchecked(current * 23 + next));
}