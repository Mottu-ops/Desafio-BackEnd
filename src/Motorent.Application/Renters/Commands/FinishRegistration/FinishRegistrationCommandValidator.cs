using Motorent.Domain.Renters.Enums;

namespace Motorent.Application.Renters.Commands.FinishRegistration;

internal sealed class FinishRegistrationCommandValidator : AbstractValidator<FinishRegistrationCommand>
{
    public FinishRegistrationCommandValidator()
    {
        RuleFor(x => x.Cnpj)
            .NotEmpty()
            .WithMessage("Must not be empty.");

        RuleFor(x => x.CnhNumber)
            .NotEmpty()
            .WithMessage("Must not be empty.")
            .Must(str => str.All(char.IsDigit))
            .WithMessage("Must contain only digits.");

        RuleFor(x => x.CnhCategory)
            .NotEmpty()
            .WithMessage("Must not be empty.")
            .Must(str => CnhCategory.TryFromName(str, false, out _))
            .WithMessage($"Must be one of the following: " +
                         $"{string.Join(", ", CnhCategory.List.Select(x => x.Name))}");
    }
}