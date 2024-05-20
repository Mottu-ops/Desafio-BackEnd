namespace MotorcycleRental.AdminManagementService.Service.UseCases.RentalPlanUseCase.UpdateRentalPlan
{
    public interface IUpdateRentalPlanUseCase
    {
        Task<RentalPlanInputOutput> Execute(UpdateRentalPlanIntput input);
    }
}
