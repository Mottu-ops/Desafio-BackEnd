namespace Job.Domain.Queries.Moto;

public sealed record MotoQuery(Guid Id, int Year, string Model, string Plate);