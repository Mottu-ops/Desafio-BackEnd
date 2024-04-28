namespace Job.Domain.Commands.Rent.Validations;

public class CreateRentValidation : AbstractValidator<CreateRentCommand>
{
    public CreateRentValidation()
    {
        RuleFor(x => x.IdMoto)
            .NotEmpty()
            .WithMessage("IdMoto é obrigatorio para criar um aluguel");

        RuleFor(x => x.DatePreview)
            .NotEmpty()
            .WithMessage("Previsão de terminio é obrigatorio para criar um aluguel")
            .GreaterThan(DateTime.Now.AddDays(1))
            .WithMessage("Previsão de terminio deve ser maior que a data atual");

        RuleFor(x => x.Plan)
            .NotEmpty()
            .WithMessage("Plano é obrigatorio para criar um aluguel");
    }
}