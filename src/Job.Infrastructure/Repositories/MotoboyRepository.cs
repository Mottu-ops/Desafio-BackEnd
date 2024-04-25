using Job.Domain.Entities.User;
using Job.Domain.Repositories;
using Job.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Job.Infrastructure.Repositories;

public class MotoboyRepository(JobContext context) : IMotoboyRepository
{
    public async Task<MotoboyEntity?> GetAsync(string cnpj, string password, CancellationToken cancellationToken)
    {
        return await context.Motoboys.Where(x => x.Cnpj == cnpj && x.Password == password).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<MotoboyEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.Motoboys.Where(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<MotoboyEntity?> GetByCnpjAsync(string cnpj, CancellationToken cancellationToken)
    {
        return await context.Motoboys.Where(x => x.Cnpj == cnpj).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> CheckCnpjExistsAsync(string cnpj, CancellationToken cancellationToken)
    {
        return await context.Motoboys.AnyAsync(x => x.Cnpj == cnpj, cancellationToken);
    }

    public async Task CreateAsync(MotoboyEntity motoboy, CancellationToken cancellationToken)
    {
        await context.Motoboys.AddAsync(motoboy, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(MotoboyEntity motoboy, CancellationToken cancellationToken)
    {
        context.Motoboys.Update(motoboy);
        await context.SaveChangesAsync(cancellationToken);
    }
}