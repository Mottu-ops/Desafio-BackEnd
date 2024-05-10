namespace Job.Domain.Commands.Moto;
public sealed record CreateMotoCommand(int Year, string Model, string Plate);