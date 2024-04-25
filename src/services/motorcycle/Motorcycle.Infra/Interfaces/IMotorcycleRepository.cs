using Motorcycle.Domain.Entities;

namespace Motorcycle.Infra.Interfaces;
public interface IMotorCycleRepository : IBaseRepository<Vehicle> {
    Task<List<Vehicle>> GetAll();
    Task<Vehicle> Get(string plateCode);
}