using Motorent.Domain.Common.ValueObjects;

namespace Motorent.Domain.Renters.ValueObjects;

public sealed class Cnpj : ValueObject
{
    public static readonly Error Invalid = Error.Validation(
        "The CNPJ is invalid.", code: "cnpj");

    private const int Length = 14;
    private const int MaxLength = 18;

    private static readonly int[] Multipliers1 = [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
    private static readonly int[] Multipliers2 = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];

    private Cnpj(string value) => Value = value;

    public string Value { get; }

    public static Result<Cnpj> Create(string value) =>
        Validate(value) is { } cnpj ? new Cnpj(cnpj) : Invalid;

    public override string ToString() => Value;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    private static string? Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length is < Length or > MaxLength)
        {
            return null;
        }

        Span<char> cnpj = stackalloc char[Length];

        // Pula caracteres não numéricos (útil para validar CNPJs formatados como "00.000.000/0000-00" sem a
        // necessidade de quebrar a formatação)
        var index = 0;
        foreach (var digit in value.Where(char.IsDigit))
        {
            cnpj[index++] = digit;
            if (index is Length)
            {
                break;
            }
        }

        if (cnpj.Length is not Length)
        {
            return null;
        }

        var sum1 = 0;
        for (var i = 0; i < 12; i++)
        {
            sum1 += (cnpj[i] - '0') * Multipliers1[i];
        }

        var rem1 = sum1 % 11;
        var vd1 = rem1 < 2 ? 0 : 11 - rem1;

        var sum2 = 0;
        for (var i = 0; i < 13; i++)
        {
            sum2 += (cnpj[i] - '0') * Multipliers2[i];
        }

        var rem2 = sum2 % 11;
        var vd2 = rem2 < 2 ? 0 : 11 - rem2;

        return vd1 == cnpj[12] - '0' && vd2 == cnpj[13] - '0'
            ? new string(cnpj)
            : null;
    }
}