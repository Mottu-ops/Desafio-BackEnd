namespace Motorent.Application.Common.Abstractions.Security;

public interface ISecurityTokenService
{
    Task<SecurityToken> GenerateTokenAsync();
}