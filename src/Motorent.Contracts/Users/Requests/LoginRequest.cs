namespace Motorent.Contracts.Users.Requests;

public sealed record LoginRequest
{
    public string Email { get; init; } = null!;

    public string Password { get; init; } = null!;
}