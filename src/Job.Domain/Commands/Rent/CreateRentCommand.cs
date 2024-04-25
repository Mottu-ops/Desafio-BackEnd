using Job.Domain.Enums;

namespace Job.Domain.Commands.Rent;

public record CreateRentCommand(Guid IdMotoboy, Guid IdMoto, DateOnly DatePreview, EPlan Plan);