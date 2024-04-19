using Motorent.Application.Common.Abstractions.Requests;
using Motorent.Contracts.Users.Responses;

namespace Motorent.Application.Users.Commands.Refresh;

public sealed record RefreshTokenCommand : ICommand<TokenResponse>, ITransactional
{
    public required string AccessToken { get; init; }
    
    public required string RefreshToken { get; init; }
}