namespace Motorent.Contracts.Renters.Responses;

public sealed record RenterResponse
{
    public string Name { get; init; } = null!;

    public DateOnly Birthdate { get; init; }

    public string Cnpj { get; init; } = null!;

    public string CnhNumber { get; init; } = null!;

    public string CnhCategory { get; init; } = null!;

    public DateOnly CnhExpirationDate { get; init; }
}