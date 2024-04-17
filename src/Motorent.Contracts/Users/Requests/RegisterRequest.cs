namespace Motorent.Contracts.Users.Requests;

public sealed record RegisterRequest
{
    public string Name { get; init; } = null!;

    public string Email { get; init; } = null!;

    public string Password { get; init; } = null!;

    public DateOnly Birthdate { get; init; }
}