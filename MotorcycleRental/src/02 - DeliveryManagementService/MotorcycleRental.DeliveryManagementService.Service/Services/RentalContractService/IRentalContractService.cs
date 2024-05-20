using MotorcycleRental.DeliveryManagementService.Service.DTOs;

namespace MotorcycleRental.DeliveryManagementService.Service.Services.RentalContractService
{
    public interface IRentalContractService
    {
        Task AddAsync(RentalContractAddDto dto);
        Task<List<RentalContractFullDto>> GetAllAsync();
        Task<RentalContractFullDto> GetByIdAsync(Guid Id);
        Task UpdateAsync(RentalContractFullDto rentalContract);
        Task DeleteAsync(Guid id);
        Task<SimulationDto> SimulateValuesContract(DateTime StartData, DateTime EndDate);
        Task<CheckTotalRentalPriceDto> CheckTotalRentalPrice(Guid PlanId, DateTime EndDate);
    }
}
