using Microsoft.EntityFrameworkCore;
using Plan.Domain.Entity;
using Plan.Infra.Context;
using Plan.Infra.interfaces;

namespace Plan.Infra.Repositories {
    public class BaseRepository<T> : IBaseRepository<T> where T : Base
{
    private readonly PlanContext _context;
    public BaseRepository(PlanContext context) {
        _context = context;
    }
    async Task<T> IBaseRepository<T>.Create(T obj)
    {
        _context.Add(obj);
        await _context.SaveChangesAsync();
        return obj;
    }

    async Task IBaseRepository<T>.Delete(long id)
    {
        var plans = await _context.Set<T>().AsNoTracking().Where(x => x.Id == id).ToListAsync();
        var plan = plans.FirstOrDefault();
        if (plan != null) {
            _context.Remove(plan);
        await _context.SaveChangesAsync();
        }
    }

    async Task<T> IBaseRepository<T>.Get(long id )
    {
        var obj = await _context.Set<T>()
            .AsNoTracking().Where(x => x.Id == id).ToListAsync();
        return obj.FirstOrDefault()!;
    }

    async Task<T> IBaseRepository<T>.Update(T obj)
    {
        _context.Entry(obj).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return obj;
    }
}
}

