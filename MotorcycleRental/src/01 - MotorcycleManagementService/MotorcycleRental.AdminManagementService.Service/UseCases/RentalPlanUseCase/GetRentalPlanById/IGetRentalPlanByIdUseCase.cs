namespace MotorcycleRental.AdminManagementService.Service.UseCases.RentalPlanUseCase.GetRentalPlanById
{
    public interface IGetRentalPlanByIdUseCase
    {
        Task<RentalPlanInputOutput> Execute(Guid Id);
    }
}
