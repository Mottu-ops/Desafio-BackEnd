using FluentValidation;
using Motorcycle.Domain.Entities;

namespace Motorcycle.Domain.Validators;

public class VehicleValidator : AbstractValidator<Vehicle> {
    public VehicleValidator() {
        RuleFor(x => x.Model)
            .NotEmpty().WithMessage("The motorcycle model can't be empty.")
            .NotNull().WithMessage("The motorcycle model can't be null.")
            .MaximumLength(80).WithMessage("The motorcycle model must have the maximum of one hundred characters.");
        
        RuleFor(x => x.Year)
            .NotEmpty().WithMessage("The year can't be empty.")
            .NotNull().WithMessage("The year can't be null.")
            .Length(4).WithMessage("TThe year must have only nine digits.");
        
        RuleFor(x => x.Color)
            .NotEmpty().WithMessage("The color can't be empty.")
            .NotNull().WithMessage("The color can't be null.")
            .MaximumLength(20).WithMessage("The color must have the maximum of twenty characters.");
        
        RuleFor(x => x.PlateCode)
            .NotEmpty().WithMessage("The plate code can't be empty.")
            .NotNull().WithMessage("The plate code can't be null.")
            .MaximumLength(80).WithMessage("The plate code must have the maximum of eighty characters.");
    }
}