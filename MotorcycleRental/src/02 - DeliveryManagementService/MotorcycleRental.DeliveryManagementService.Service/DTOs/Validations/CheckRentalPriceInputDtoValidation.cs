using FluentValidation;

namespace MotorcycleRental.DeliveryManagementService.Service.DTOs.Validations
{
    public class CheckRentalPriceInputDtoValidation : AbstractValidator<CheckRentalPriceInputDto>
    {
        public CheckRentalPriceInputDtoValidation()
        {
            RuleFor(e => e.ContractId)
               .NotEmpty()
                .WithMessage("{PropertyName}: cannot be empty!")
                .NotNull()
                .WithMessage("{PropertyName}: Cannot be Null!");

            RuleFor(e => e.EndDate)
               .Must(BeInTheFuture)
               .WithMessage("The end date must be greater than or equal to the current date.");
        }

        private bool BeInTheFuture(DateTime endDate)
        {
            return endDate >= DateTime.Now;
        }
    }
}
