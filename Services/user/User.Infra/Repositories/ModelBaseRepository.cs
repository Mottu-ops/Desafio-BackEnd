using Microsoft.EntityFrameworkCore;
using User.Domain.Entities;
using User.Infra.Context;
using User.Infra.Interfaces;


namespace User.Infra.Repositories {
    public class ModelBaseRepository<T> : IBaseRepository<T> where T : ModelBase
{
    private readonly UserContext _context;
    public ModelBaseRepository(UserContext context) {
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
        var users = await _context.Set<T>().AsNoTracking().Where(x => x.Id == id).ToListAsync();
        var user = users.FirstOrDefault();
        if (user != null) {
            _context.Remove(user);
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

