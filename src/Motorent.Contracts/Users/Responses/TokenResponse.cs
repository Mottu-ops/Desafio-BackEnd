namespace Motorent.Contracts.Users.Responses;

public sealed record TokenResponse
{
    public string AccessToken { get; init; } = null!;

    public int ExpiresIn { get; init; }
}