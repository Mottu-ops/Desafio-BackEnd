using FluentValidation;

namespace MotorcycleRental.DeliveryManagementService.Service.DTOs.Validations
{
    public class SimulationPeriodDtoValidation : AbstractValidator<SimulationPeriodDto>
    {
        public SimulationPeriodDtoValidation()
        {
            RuleFor(e => e.StartDate)
                .Must(BeInTheFuture)
                .WithMessage("The start date must be greater than or equal to the current date.");

            RuleFor(e => e.EndDate)
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
