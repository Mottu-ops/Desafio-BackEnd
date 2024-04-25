using Plan.Domain.Entity;

namespace Plan.Infra.interfaces
{
    public interface IPlanRepository : IBaseRepository<RentPlan>{
        Task<List<RentPlan>> GetAll();
    }
}