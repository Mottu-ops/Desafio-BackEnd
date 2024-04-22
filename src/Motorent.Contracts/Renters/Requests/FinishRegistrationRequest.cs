namespace Motorent.Contracts.Renters.Requests;

public sealed record FinishRegistrationRequest
{
    public string Cnpj { get; init; } = null!;

    public string CnhNumber { get; init; } = null!;

    public string CnhCategory { get; init; } = null!;

    public DateOnly CnhExpirationDate { get; init; }
}