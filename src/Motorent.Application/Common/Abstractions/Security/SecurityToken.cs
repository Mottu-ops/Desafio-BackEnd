namespace Motorent.Application.Common.Abstractions.Security;

public sealed record SecurityToken(string TokenType, string AccessToken, int ExpiresIn);