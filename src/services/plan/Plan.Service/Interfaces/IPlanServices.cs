using Plan.Service.DTO;

namespace Plan.Service.Interfaces;

public interface IPlanServices{
    Task<PlanDto> Create(PlanDto planDto);
    Task<PlanDto> Update(PlanDto planDto);
    Task Remove(long id, long userId);
    Task<PlanDto> Get(long id, long userId);
    Task<List<PlanDto>> GetAll(long userId);
} 