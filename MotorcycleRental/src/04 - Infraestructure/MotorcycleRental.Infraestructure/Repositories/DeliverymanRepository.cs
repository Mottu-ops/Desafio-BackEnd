using Microsoft.EntityFrameworkCore;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Interfaces;
using MotorcycleRental.Infraestructure.Context.Postgress;

namespace MotorcycleRental.Infraestructure.Repositories
{
    public class DeliverymanRepository : BaseRepository<Deliveryman>, IDeliverymanRepository
    {
        private readonly ApplicationDbContext _context;
        public DeliverymanRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Deliveryman> GetByCnhAsync(string cnh)
        {
            Deliveryman retorno = await _context.Deliverymen.Where(p => p.DriverLicenseNumber == cnh && p.IsActived == true).FirstOrDefaultAsync();
            return retorno;
        }

        public async Task<Deliveryman> GetByCnpjAsync(string cnpj)
        {
            Deliveryman retorno = await _context.Deliverymen.Where(p => p.CNPJ == cnpj && p.IsActived == true).FirstOrDefaultAsync();
            return retorno;
        }
    }
}
