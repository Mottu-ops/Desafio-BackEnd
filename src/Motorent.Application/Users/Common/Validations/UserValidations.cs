namespace Motorent.Application.Users.Common.Validations;

internal static class UserValidations
{
    public static IRuleBuilderOptions<T, string> Role<T>(this IRuleBuilder<T, string> builder)
    {
        return builder
            .Must(name => Domain.Users.Enums.Role.TryFromName(name, out var role) &&
                          Domain.Users.Enums.Role.Valid.Contains(role))
            .WithMessage($"Must be one of the following: " +
                         $"{string.Join(", ", Domain.Users.Enums.Role.Valid.Select(r => r.Name))}.");
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
}