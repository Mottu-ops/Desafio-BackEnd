namespace MotorcycleRental.AdminManagementService.Service.UseCases.MotorcycleUseCase.GetAllMotorcycle
{
    public interface IGetAllMotorcycleUseCase
    {
        Task<List<MotorcycleInputOutput>> Execute();
    }
}
