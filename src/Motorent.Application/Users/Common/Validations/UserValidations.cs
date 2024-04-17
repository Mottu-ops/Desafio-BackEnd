namespace Motorent.Application.Users.Common.Validations;

internal static class UserValidations
{
    public static IRuleBuilderOptions<T, string> Role<T>(this IRuleBuilder<T, string> builder)
    {
        return builder
            .Must(name => Domain.Users.Enums.Role.IsDefined(name))
            .WithMessage($"Must be one of the following: " +
                         $"{string.Join(", ", Domain.Users.Enums.Role.List.Select(r => r.Name))}.");
    }

    public static IRuleBuilderOptions<T, string> Name<T>(this IRuleBuilder<T, string> builder)
    {
        return builder
            .NotEmpty()
            .WithMessage("Must not be empty.")
            .Length(min: 2, max: 50)
            .WithMessage("Must be between 2 and 50 characters.")
            .Matches(@"^(?!.*['.]$)[\p{L}'. }]+$")
            .WithMessage("Must contain only letters and spaces, periods and apostrophes followed by letters.");
    }

    public static IRuleBuilderOptions<T, string> Email<T>(this IRuleBuilder<T, string> builder)
    {
        return builder
            .NotEmpty()
            .WithMessage("Must not be empty.")
            .EmailAddress()
            .WithMessage("Must be a valid email address.");
    }

    public static IRuleBuilderOptions<T, string> Password<T>(this IRuleBuilder<T, string> builder)
    {
        return builder
            .NotEmpty()
            .WithMessage("Must not be empty.")
            .MinimumLength(8)
            .WithMessage("Must be at least 8 characters long.")
            .Matches(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).*$")
            .WithMessage("Must contain at least one uppercase letter, one lowercase letter and one digit.");
    }

    public static IRuleBuilderOptions<T, DateOnly> Birthdate<T>(this IRuleBuilder<T, DateOnly> builder)
    {
        return builder
            .NotEmpty()
            .WithMessage("Must not be empty.")
            .Must(bd =>
            {
                var now = DateTime.Today;
                var age = now.Year - bd.Year;

                // If the birthday hasn't happened yet this year, subtract one year
                if (now.Month < bd.Month || (now.Month == bd.Month && now.Day < bd.Day))
                {
                    age--;
                }

                return age >= 18;
            })
            .WithMessage("Must be at least 18 years old.");
    }
}