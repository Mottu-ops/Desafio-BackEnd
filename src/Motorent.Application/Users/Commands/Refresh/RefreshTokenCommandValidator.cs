namespace Motorent.Application.Users.Commands.Refresh;

internal sealed class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(x => x.AccessToken)
            .NotEmpty()
            .WithMessage("Must not be empty");
        
        RuleFor(x => x.RefreshToken)
            .NotEmpty()
            .WithMessage("Must not be empty");
    }
}