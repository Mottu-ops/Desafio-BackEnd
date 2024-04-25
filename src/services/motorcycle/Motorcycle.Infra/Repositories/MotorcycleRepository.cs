using Microsoft.EntityFrameworkCore;
using Motorcycle.Domain.Entities;
using Motorcycle.Infra.Context;
using Motorcycle.Infra.Interfaces;

namespace Motorcycle.Infra.Repositories;

public class MotorcycleRepository : BaseRepository<Vehicle>, IMotorCycleRepository
{
    private readonly MotorcycleContext _context;
    public MotorcycleRepository(MotorcycleContext context) : base(context)
    {
        _context = context;
    }
    public async Task<Vehicle> Get(long id) => ( 
        await _context.Motorcycles.AsNoTracking()
            .Where(motorcycle => motorcycle.Id == id
        ).ToListAsync()).FirstOrDefault()!;
    public async Task<Vehicle> Get(string plateCode) => ( 
        await _context.Motorcycles.AsNoTracking()
            .Where(motorcycle => motorcycle.PlateCode == plateCode
        ).ToListAsync()).FirstOrDefault()!;


    Task<List<Vehicle>> IMotorCycleRepository.GetAll()
    {
        var vehicles = _context.Motorcycles.AsNoTracking().ToListAsync();
        return vehicles;
    }
}