using FluentValidation;

namespace MotorcycleRental.AdminManagementService.Service.UseCases.MotorcycleUseCase.Validations
{
    public class GetMotorcycleByPlateInputValidation : AbstractValidator<GetMotorcycleByPlateInput>
    {
        public GetMotorcycleByPlateInputValidation()
        {
            RuleFor(m => m.Plate)
                .NotEmpty()
                .WithMessage("{PropertyName}: cannot be empty!")
                .NotNull()
                .WithMessage("{PropertyName}: Cannot be Null!");
        }
    }
}
