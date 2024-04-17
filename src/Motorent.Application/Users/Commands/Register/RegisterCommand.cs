using Motorent.Application.Common.Abstractions.Requests;
using Motorent.Contracts.Users.Responses;

namespace Motorent.Application.Users.Commands.Register;

public sealed record RegisterCommand : ICommand<TokenResponse>, ITransactional
{
    public required string Name { get; init; }

    public required string Email { get; init; }

    public required string Password { get; init; }

    public required DateOnly Birthdate { get; init; }
}