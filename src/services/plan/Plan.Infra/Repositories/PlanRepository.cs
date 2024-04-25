using Microsoft.EntityFrameworkCore;
using Plan.Domain.Entity;
using Plan.Infra.Context;
using Plan.Infra.interfaces;

namespace Plan.Infra.Repositories{
    public class PlanRepository : BaseRepository<RentPlan>, IPlanRepository
{
    private readonly PlanContext _context;
    public PlanRepository(PlanContext context) : base(context)
    {
        _context = context;
    }
        Task<List<RentPlan>> IPlanRepository.GetAll()
    {
            var plans = _context.Plans.AsNoTracking().ToListAsync();
            return plans;
        }
}
}

