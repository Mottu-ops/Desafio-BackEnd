using Motorent.Application.Common.Abstractions.Requests;
using Motorent.Contracts.Renters.Responses;

namespace Motorent.Application.Renters.Commands.FinishRegistration;

public sealed record FinishRegistrationCommand : ICommand<RenterResponse>, ITransactional
{
    public required string Cnpj { get; init; }

    public required string CnhNumber { get; init; }

    public required string CnhCategory { get; init; }

    public required DateOnly CnhExpirationDate { get; init; }
}