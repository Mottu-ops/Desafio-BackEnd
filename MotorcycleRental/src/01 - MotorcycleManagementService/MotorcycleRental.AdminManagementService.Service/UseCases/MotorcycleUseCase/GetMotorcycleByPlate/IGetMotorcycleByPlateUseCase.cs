namespace MotorcycleRental.AdminManagementService.Service.UseCases.MotorcycleUseCase.GetMotorcycleByPlate
{
    public interface IGetMotorcycleByPlateUseCase
    {
        Task<MotorcycleInputOutput> Execute(GetMotorcycleByPlateInput input);
    }
}
