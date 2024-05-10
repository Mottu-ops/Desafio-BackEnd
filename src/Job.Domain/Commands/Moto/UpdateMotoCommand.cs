namespace Job.Domain.Commands.Moto;

public sealed record UpdateMotoCommand(Guid Id, int Year, string Model, string Plate);