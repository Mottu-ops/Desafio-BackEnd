using Motorent.Domain.Common.ValueObjects;

namespace Motorent.Domain.Renters.ValueObjects;

public sealed class CnhNumber : ValueObject
{
    public static readonly Error Invalid = Error.Validation("Invalid CNH number.", code: "cnh");

    private const int Length = 11;

    private const string Ones = "11111111111";
    
    public required string Value { get; init; }

    public static Result<CnhNumber> Create(string value) =>
        Validate(value) is not { } cnh ? Invalid : new CnhNumber { Value = cnh };

    public override string ToString() => Value;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    private static string? Validate(ReadOnlySpan<char> cnh)
    {
        if (cnh.Length is not Length || cnh.IsWhiteSpace() || cnh.SequenceEqual(Ones))
        {
            return null;
        }

        var sum1 = 0;
        for (int i = 0, j = 9; i < 9; i++, j--)
        {
            sum1 += (cnh[i] - '0') * j;
        }

        var rem1 = sum1 % 11;
        var vd1 = rem1 >= 10 ? 0 : rem1;

        var sum2 = 0;
        for (int i = 0, j = 1; i < 9; i++, j++)
        {
            sum2 += (cnh[i] - '0') * j;
        }

        var rem2 = sum2 % 11;
        var vd2 = rem2 >= 10 ? 0 : rem2;

        return vd1 * 10 + vd2 == int.Parse(cnh.Slice(9, 2))
            ? cnh.ToString()
            : null;
    }
}