namespace MotorcycleRental.AdminManagementService.Service.UseCases.MotorcycleUseCase.AddMotorcycle
{
    public interface IAddMotorcycleUseCase
    {
        Task<AddMotorcycleOutput> Execute(AddMotorcycleInput input);
    }
}
