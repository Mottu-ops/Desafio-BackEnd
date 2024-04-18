namespace Motorent.Application.Users.Commands.Login;

internal sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Must not be empty.");
        
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Must not be empty.");
    }
}