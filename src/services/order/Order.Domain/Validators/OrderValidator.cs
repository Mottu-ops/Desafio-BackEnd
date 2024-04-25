
using FluentValidation;
using Order.Domain.Entities;

namespace Order.Domain.Validators;

public class OrderValidator : AbstractValidator<OrderEntity> {
    public OrderValidator() {
        RuleFor(x => x.Price)
            .NotEmpty().WithMessage("The price model can't be empty.")
            .NotNull().WithMessage("The price model can't be null.");
        
        RuleFor(x => x.Situation)
            .NotEmpty().WithMessage("The order situation can't be empty.")
            .NotNull().WithMessage("The order situation can't be null.")
            .Length(4).WithMessage("TThe order situation must have only nine digits.");
    }
}