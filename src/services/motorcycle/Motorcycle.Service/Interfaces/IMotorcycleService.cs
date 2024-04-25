using Motorcycle.Service.DTO;

namespace Motorcycle.Service.Interfaces;

public interface IMotorcycleServices {
    Task<VehicleDto> Create(VehicleDto VehicleDTO);
    Task<VehicleDto> Update(VehicleDto VehicleDTO);
    Task Remove(long id, long userId);
    Task<VehicleDto> Get(long id, long userId);
    Task<VehicleDto> Get(string plateCode, long userId);
    Task<List<VehicleDto>> GetAll(long userId);
}