

namespace Job.Domain.Commands.Moto.Validations;

public class CreateMotoValidation : AbstractValidator<CreateMotoCommand>
{
    public CreateMotoValidation()
    {
        RuleFor(x => x.Year)
            .NotEmpty()
            .WithMessage("Ano é obrigatório")
            .GreaterThan(1900)
            .WithMessage("Ano deve ser maior que 1900");

        RuleFor(x => x.Model)
            .NotEmpty()
            .WithMessage("Modelo é obrigatório");

        RuleFor(x => x.Plate)
            .NotEmpty()
            .WithMessage("Placa é obrigatória");
    }
}