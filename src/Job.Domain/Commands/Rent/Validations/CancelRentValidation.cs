namespace Job.Domain.Commands.Rent.Validations;

public class CancelRentValidation : AbstractValidator<CancelRentCommand>
{
    public CancelRentValidation()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id é obrigatorio para cancelar um aluguel");

        RuleFor(x => x.DatePreview)
            .NotEmpty()
            .WithMessage("Previsão de terminio é obrigatorio para cancelar um aluguel")
            .GreaterThan(DateTime.Now)
            .WithMessage("Previsão de terminio deve ser maior que a data atual");
    }
}