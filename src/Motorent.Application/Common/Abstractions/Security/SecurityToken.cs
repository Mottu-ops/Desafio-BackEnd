namespace Motorent.Application.Common.Abstractions.Security;

public sealed record SecurityToken(
    string AccessToken,
    string RefreshToken,
    int ExpiresIn,
    string TokenType = "Bearer");