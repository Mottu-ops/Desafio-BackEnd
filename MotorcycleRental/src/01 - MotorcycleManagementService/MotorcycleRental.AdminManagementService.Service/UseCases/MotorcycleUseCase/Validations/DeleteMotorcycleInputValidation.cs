using FluentValidation;

namespace MotorcycleRental.AdminManagementService.Service.UseCases.MotorcycleUseCase.Validations
{
    public class DeleteMotorcycleInputValidation : AbstractValidator<DeleteMotorcycleInput>
    {
        public DeleteMotorcycleInputValidation()
        {
            RuleFor(m => m.Id)
                .NotEmpty()
                .WithMessage("{PropertyName}: cannot be empty!")
                .NotNull()
                .WithMessage("{PropertyName}: Cannot be Null!");
        }
    }
}
