namespace Motorent.Application.Common.Abstractions.Security;

public sealed record SecurityToken(string AccessToken, int ExpiresIn);