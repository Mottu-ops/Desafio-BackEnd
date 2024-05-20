using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Interfaces;
using MotorcycleRental.Infraestructure.Context.Postgress;

namespace MotorcycleRental.Infraestructure.Repositories
{
    public class RentalPlanRepository : BaseRepository<RentalPlan>, IRentalPlanRepository
    {
        public RentalPlanRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
