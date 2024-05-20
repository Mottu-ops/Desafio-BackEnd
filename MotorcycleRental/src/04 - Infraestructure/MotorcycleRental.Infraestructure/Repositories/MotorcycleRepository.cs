using Microsoft.EntityFrameworkCore;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Interfaces;
using MotorcycleRental.Infraestructure.Context.Postgress;

namespace MotorcycleRental.Infraestructure.Repositories
{
    public class MotorcycleRepository : BaseRepository<Motorcycle>, IMotorcycleRepository
    {
        private readonly ApplicationDbContext _context;
        public MotorcycleRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Motorcycle> GetByPlateAsync(string Plate)
        {
            Motorcycle retorno = await _context.Motorcycles.Where(p => p.Plate == Plate && p.IsActived == true).FirstOrDefaultAsync();
            return retorno;
        }
    }
}
