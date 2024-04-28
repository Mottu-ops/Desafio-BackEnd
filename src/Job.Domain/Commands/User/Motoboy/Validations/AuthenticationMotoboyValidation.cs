using Job.Domain.Commons;

namespace Job.Domain.Commands.User.Motoboy.Validations;

public class AuthenticationMotoboyValidation : AbstractValidator<AuthenticationMotoboyCommand>
{
    public AuthenticationMotoboyValidation()
    {
        RuleFor(x => x.Cnpj)
            .NotEmpty()
            .WithMessage("Cnpj é obrigatório")
            .Must(Cnpj.IsCnpj)
            .WithMessage("Cnpj inválido");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Senha é obrigatório")
            .MinimumLength(6)
            .WithMessage("Senha deve ter no mínimo 6 caracteres");
    }
}