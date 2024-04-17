namespace Motorent.Domain.Users.Errors;

internal static class UserErrors
{
    public static Error DuplicateEmail(string email) => Error.Conflict(
        "O endereço de e-email já está em uso por outro usuário.",
        code: "user.duplicate_email",
        details: new() { ["email"] = email });
}