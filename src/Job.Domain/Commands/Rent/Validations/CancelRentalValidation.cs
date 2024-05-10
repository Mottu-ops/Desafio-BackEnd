namespace Job.Domain.Commands.Rent.Validations;

public sealed class CancelRentalValidation : AbstractValidator<CancelRentCommand>
{
    public CancelRentalValidation()
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