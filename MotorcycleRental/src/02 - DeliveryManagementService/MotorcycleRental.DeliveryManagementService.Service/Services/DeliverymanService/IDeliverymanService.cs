using MotorcycleRental.DeliveryManagementService.Service.DTOs;

namespace MotorcycleRental.DeliveryManagementService.Service.Services.DeliverymanService
{
    public interface IDeliverymanService
    {
        Task AddAsync(DeliverymanAddDto deliverymanDto);
        Task<List<DeliverymanFullDto>> GetAllAsync();
        Task<DeliverymanFullDto> GetByIdAsync(Guid id);
        Task UpdateAsync(DeliverymanFullDto deliverymanDto);
        Task DeleteAsync(Guid id);
        Task AddPhotoCnh(DeliverymanAddPhotoDto deliverymanAddPhoto);
    }
}
