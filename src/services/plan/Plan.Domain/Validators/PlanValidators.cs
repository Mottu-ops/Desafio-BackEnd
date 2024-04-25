using FluentValidation;
using Plan.Domain.Entity;

namespace Plan.Domain.Validators;
public class PlanValidator : AbstractValidator<RentPlan> {
    public PlanValidator() {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("The name can't be empty.")
            .NotNull().WithMessage("The name can't be null.")
            .MaximumLength(80).WithMessage("The name must have the maximum of one hundred characters.")
            .MinimumLength(2).WithMessage("The name must have at least two characters.");

        RuleFor(x => x.Days)
            .NotEmpty().WithMessage("The days can't be empty.")
            .NotNull().WithMessage("The days can't be null.");

        RuleFor(x => x.DailyRate)
            .NotEmpty().WithMessage("The daily rate can't be empty.")
            .NotNull().WithMessage("The daily rate can't be null.");
    
        RuleFor(x => x.User)
            .NotEmpty().WithMessage("The user rate can't be empty.")
            .NotNull().WithMessage("The user rate can't be null.");

        RuleFor(x => x.LateFee)
            .NotEmpty().WithMessage("The late fee can't be empty.")
            .NotNull().WithMessage("The late fee can't be null.");
    }
}