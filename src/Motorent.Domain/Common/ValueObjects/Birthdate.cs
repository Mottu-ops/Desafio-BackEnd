namespace Motorent.Domain.Common.ValueObjects;

public sealed class Birthdate : ValueObject
{
    public static readonly Error MustBeAtLeast18YearsOld = Error.Validation(
        "Must be at least 18 years old.", code: "birthdate");

    private Birthdate()
    {
    }

    public DateOnly Value { get; private init; }

    public static Result<Birthdate> Create(DateOnly date) => CalculateAge(date) >= 18
        ? new Birthdate { Value = date }
        : MustBeAtLeast18YearsOld;

    public override string ToString() => Value.ToString("yyyy-MM-dd");

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    private static int CalculateAge(DateOnly date)
    {
        var now = DateTime.Today;
        var age = now.Year - date.Year;

        if (now.Month < date.Month || (now.Month == date.Month && now.Day < date.Day))
        {
            age--;
        }

        return age;
    }
}