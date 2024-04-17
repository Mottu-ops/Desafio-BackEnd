using Motorent.Application.Users.Common.Validations;

namespace Motorent.Application.Users.Commands.Register;

internal sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Name)
            .Name();

        RuleFor(x => x.Email)
            .Email();

        RuleFor(x => x.Password)
            .Password();

        RuleFor(x => x.Birthdate)
            .Birthdate();
    }
}