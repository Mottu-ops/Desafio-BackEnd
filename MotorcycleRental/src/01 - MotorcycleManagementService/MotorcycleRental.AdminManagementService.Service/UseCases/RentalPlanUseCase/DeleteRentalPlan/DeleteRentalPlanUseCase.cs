
using MotorcycleRental.AdminManagementService.Service.Exceptions;

namespace MotorcycleRental.AdminManagementService.Service.UseCases.RentalPlanUseCase.DeleteRentalPlan
{
    public class DeleteRentalPlanUseCase : IDeleteRentalPlanUseCase
    {
        private readonly IRentalPlanRepository _rentalPlanRepository;

        public DeleteRentalPlanUseCase(IRentalPlanRepository rentalPlanRepository)
        {
            _rentalPlanRepository = rentalPlanRepository;
        }

        public async Task Execute(Guid Id)
        {
            RentalPlan rentalPlan = await _rentalPlanRepository.GetByIdAsync(Id);

            if (rentalPlan == null)
                throw new NotFoundException("This mentioned rental plan does not exist");

            await _rentalPlanRepository.DeleteAsync(rentalPlan);
        }
    }
}
