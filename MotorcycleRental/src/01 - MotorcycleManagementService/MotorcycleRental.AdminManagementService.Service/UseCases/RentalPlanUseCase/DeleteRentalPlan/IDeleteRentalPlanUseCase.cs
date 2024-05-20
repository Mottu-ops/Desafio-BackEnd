namespace MotorcycleRental.AdminManagementService.Service.UseCases.RentalPlanUseCase.DeleteRentalPlan
{
    public interface IDeleteRentalPlanUseCase
    {
        Task Execute(Guid Id);
    }
}
