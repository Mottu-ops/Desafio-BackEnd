using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Interfaces;
using MotorcycleRental.Infraestructure.Context.Postgress;

namespace MotorcycleRental.Infraestructure.Repositories
{
    public class RentalContractRepository : BaseRepository<RentalContract>, IRentalContractRepository
    {
        public RentalContractRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
