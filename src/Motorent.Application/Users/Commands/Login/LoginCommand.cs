using Motorent.Application.Common.Abstractions.Requests;
using Motorent.Contracts.Users.Responses;

namespace Motorent.Application.Users.Commands.Login;

public sealed record LoginCommand : ICommand<TokenResponse>
{
    public required string Email { get; init; }

    public required string Password { get; init; }
}