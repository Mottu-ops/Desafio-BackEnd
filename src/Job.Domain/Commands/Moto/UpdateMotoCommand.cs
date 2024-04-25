namespace Job.Domain.Commands.Moto;

public record UpdateMotoCommand(Guid Id, int Year, string Model, string Plate);