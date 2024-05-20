using FluentValidation;
using MotorcycleRental.AdminManagementService.Service.UseCases.RentalPlanUseCase.UpdateRentalPlan;

namespace MotorcycleRental.AdminManagementService.Service.UseCases.RentalPlanUseCase.Validations
{
    public class UpdateRentalPlanValidation : AbstractValidator<UpdateRentalPlanIntput>
    {
        public UpdateRentalPlanValidation()
        {
            RuleFor(m => m.Descrition)
               .NotEmpty()
                .WithMessage("{PropertyName}: cannot be empty!")
                .NotNull()
                .WithMessage("{PropertyName}: Cannot be Null!");

            RuleFor(m => m.Days)
                .GreaterThan(0)
                .WithMessage("{PropertyName}: Required a value greater than 0!");

            RuleFor(m => m.DayValue)
                .GreaterThan(0)
                .WithMessage("{PropertyName}: Required a value greater than 0!");

            RuleFor(m => m.PercentageFine)
                .GreaterThan(0)
                .WithMessage("{PropertyName}: Required a value greater than 0!");

            RuleFor(m => m.AdditionalValueDaily)
                .GreaterThan(0)
                .WithMessage("{PropertyName}: Required a value greater than 0!");
        }
    }
}
