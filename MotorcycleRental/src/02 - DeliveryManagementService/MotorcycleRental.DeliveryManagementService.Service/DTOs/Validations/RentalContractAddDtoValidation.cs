using FluentValidation;

namespace MotorcycleRental.DeliveryManagementService.Service.DTOs.Validations
{
    public class RentalContractAddDtoValidation : AbstractValidator<RentalContractAddDto>
    {
        public RentalContractAddDtoValidation()
        {
            RuleFor(e => e.DeliverymanId)
               .NotEmpty()
                .WithMessage("{PropertyName}: cannot be empty!")
                .NotNull()
                .WithMessage("{PropertyName}: Cannot be Null!");

            RuleFor(e => e.RentanPlanId)
               .NotEmpty()
                .WithMessage("{PropertyName}: cannot be empty!")
                .NotNull()
                .WithMessage("{PropertyName}: Cannot be Null!");
            RuleFor(e => e.MotorcycleId)
               .NotEmpty()
                .WithMessage("{PropertyName}: cannot be empty!")
                .NotNull()
                .WithMessage("{PropertyName}: Cannot be Null!");

            RuleFor(e => e.StartDate)
                .Must(BeInTheFuture)
                .WithMessage("The start date must be greater than or equal to the current date.");
        }

        private bool BeInTheFuture(DateTime startDate)
        {
            return startDate >= DateTime.Now;
        }
    }
}
