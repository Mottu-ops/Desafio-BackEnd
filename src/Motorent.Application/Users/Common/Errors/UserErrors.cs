namespace Motorent.Application.Users.Common.Errors;

internal static class UserErrors
{
    public static readonly Error InvalidCredentials = Error.Unauthorized(
        "Invalid email or password.", code: "user.invalid_credentials");
}