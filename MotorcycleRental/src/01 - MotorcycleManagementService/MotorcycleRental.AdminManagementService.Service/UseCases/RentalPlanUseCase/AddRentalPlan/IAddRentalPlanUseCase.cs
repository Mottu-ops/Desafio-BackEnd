namespace MotorcycleRental.AdminManagementService.Service.UseCases.RentalPlanUseCase.AddRentalPlan
{
    public interface IAddRentalPlanUseCase
    {
        Task<AddRentalPlanOutput> Execute(AddRentalPlanInput input);
    }
}
