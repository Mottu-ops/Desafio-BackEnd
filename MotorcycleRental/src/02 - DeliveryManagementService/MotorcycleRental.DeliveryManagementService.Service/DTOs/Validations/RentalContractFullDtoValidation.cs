using FluentValidation;

namespace MotorcycleRental.DeliveryManagementService.Service.DTOs.Validations
{
    public class RentalContractFullDtoValidation : AbstractValidator<RentalContractFullDto>
    {
        public RentalContractFullDtoValidation()
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

            RuleFor(e => e.ExpectedEndDate)
                .Must((eventInstance, endDate) => BeGreaterThanStartDate(eventInstance.StartDate, endDate))
                .WithMessage("The end date must be greater than the start date.");
        }
        private bool BeInTheFuture(DateTime startDate)
        {
            return startDate >= DateTime.Now;
        }

        private bool BeGreaterThanStartDate(DateTime startDate, DateTime endDate)
        {
            return endDate > startDate;
        }
    }
}