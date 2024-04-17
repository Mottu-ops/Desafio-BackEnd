namespace Motorent.Domain.Users.Errors;

internal static class UserErrors
{
    public static Error DuplicateEmail(string email) => Error.Conflict(
        "The email address is already in use.",
        code: "user.duplicate_email",
        details: new() { ["email"] = email });
}