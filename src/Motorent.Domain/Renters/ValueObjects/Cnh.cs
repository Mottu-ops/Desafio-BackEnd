using Motorent.Domain.Common.ValueObjects;
using Motorent.Domain.Renters.Enums;

namespace Motorent.Domain.Renters.ValueObjects;

public sealed class Cnh : ValueObject
{
    public static readonly Error Expired = Error.Validation(
        "The CNH is expired.", code: "cnh");

    public required CnhNumber Number { get; init; }

    public required CnhCategory Category { get; init; }

    public required DateOnly ExpirationDate { get; init; }

    public static Result<Cnh> Create(CnhNumber number, DateOnly expirationDate, CnhCategory category)
    {
        if (CheckExpirationDate(expirationDate))
        {
            return Expired;
        }

        return new Cnh
        {
            Number = number,
            Category = category,
            ExpirationDate = expirationDate
        };
    }

    public override string ToString() => $"{Number} - {ExpirationDate} - {Category}";

    private static bool CheckExpirationDate(DateOnly experiationDate) =>
        experiationDate < DateOnly.FromDateTime(DateTime.UtcNow);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Category;
        yield return Number;
        yield return ExpirationDate;
    }
}