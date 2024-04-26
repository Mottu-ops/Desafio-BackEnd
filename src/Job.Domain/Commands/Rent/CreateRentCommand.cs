using Job.Domain.Enums;

namespace Job.Domain.Commands.Rent;

public record CreateRentCommand(Guid IdMoto, DateTime DatePreview, EPlan Plan)
{
    public string Cnpj { get; set; } = default!;
}