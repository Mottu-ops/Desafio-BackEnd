using Job.Domain.Commons;

namespace Job.Domain.Commands.User.Motoboy.Validations;

public sealed class CreateMotoboyValidation : AbstractValidator<CreateMotoboyCommand>
{
    public CreateMotoboyValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Nome é obrigatório");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Senha é obrigatória")
            .MinimumLength(6)
            .WithMessage("Senha deve ter no mínimo 6 caracteres");

        RuleFor(x => x.Cnpj)
            .NotEmpty()
            .WithMessage("Cnpj é obrigatório")
            .Must(CnpjValidation.IsCnpj)
            .WithMessage("Cnpj inválido");

        RuleFor(x => x.DateBirth)
            .NotEmpty()
            .WithMessage("Data de nascimento é obrigatória")
            .LessThanOrEqualTo(DateTime.Now.AddYears(-18))
            .WithMessage("Data de nascimento deve ser maior que 18 anos");

        RuleFor(x => x.Cnh)
            .NotEmpty()
            .WithMessage("Cnh é obrigatório")
            .Must(CnhValidation.IsCnh)
            .WithMessage("CNH inválida");

        RuleFor(x => x.TypeCnh)
            .NotEmpty()
            .WithMessage("Tipo de cnh é obrigatório");
    }
}