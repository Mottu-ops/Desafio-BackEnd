namespace Motorent.Contracts.Users.Requests;

public sealed record RefreshTokenRequest
{
    public string AccessToken { get; init; } = null!;
    
    public string RefreshToken { get; init; } = null!;
}