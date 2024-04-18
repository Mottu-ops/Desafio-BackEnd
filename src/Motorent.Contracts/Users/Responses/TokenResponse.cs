namespace Motorent.Contracts.Users.Responses;

public sealed record TokenResponse
{
    public string TokenType { get; init; } = null!;
    
    public string AccessToken { get; init; } = null!;

    public int ExpiresIn { get; init; }
}