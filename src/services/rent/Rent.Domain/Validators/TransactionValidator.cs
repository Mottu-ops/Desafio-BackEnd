using FluentValidation;
using Rent.Domain.Entities;

namespace Rent.Domain.Validators;
public class TransactionValidator : AbstractValidator<Transaction> {
    public TransactionValidator() {
        RuleFor(x => x.Manager)
            .NotEmpty().WithMessage("The manager is required")
            .NotNull().WithMessage("The manager can't be null");
    
        RuleFor(x => x.DeliveryMan)
            .NotEmpty().WithMessage("The delivery man is required")
            .NotNull().WithMessage("The delivery man can't be null");
    
        RuleFor(x => x.Motorcycle)
            .NotEmpty().WithMessage("The locator is required")
            .NotNull().WithMessage("The locator can't be null");
        
        RuleFor(x => x.Plan)
            .NotEmpty().WithMessage("The motorcycle is required")
            .NotNull().WithMessage("The motorcycle can't be null");
    }
}