using FluentValidation;

namespace MotorcycleRental.AdminManagementService.Service.UseCases.MotorcycleUseCase.Validations
{
    public class UpdateMotorcycleInputValidation : AbstractValidator<MotorcycleInputOutput>
    {
        public UpdateMotorcycleInputValidation()
        {
            RuleFor(m => m.Year.ToString())
                .MinimumLength(4).WithMessage("{PropertyName}: Year in YYYY format") 
                .MaximumLength(4).WithMessage("{PropertyName}: Year in YYYY format");

            RuleFor(m => m.Model)
                .NotEmpty()
                .WithMessage("{PropertyName}: cannot be empty!")
                .NotNull()
                .WithMessage("{PropertyName}: Cannot be Null!");

            RuleFor(m => m.Plate)
                .NotEmpty()
                .WithMessage("{PropertyName}: cannot be empty!")
                .NotNull()
                .WithMessage("{PropertyName}: Cannot be Null!")
                .MinimumLength(7)
                .WithMessage("{PropertyName}: Minimum 7 characters!");
        }
    }
}
