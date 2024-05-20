namespace MotorcycleRental.AdminManagementService.Service.UseCases.MotorcycleUseCase.UpdateMotocycle
{
    public interface IUpdateMotorcycleUseCase
    {
        Task<MotorcycleInputOutput> Execute(MotorcycleInputOutput input);
    }
}
