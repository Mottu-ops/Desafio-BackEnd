namespace MotorcycleRental.AdminManagementService.Service.UseCases.RentalPlanUseCase.GetAllRentalPlan
{
    public interface IGetAllRentalPlanUseCase
    {
        Task<List<RentalPlanInputOutput>> Execute();
    }
}
