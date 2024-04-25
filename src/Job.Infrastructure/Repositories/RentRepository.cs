using Job.Domain.Entities.Rent;
using Job.Domain.Repositories;
using Job.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Job.Infrastructure.Repositories;

public class RentRepository(JobContext context) : IRentRepository
{
    public async Task CreateAsync(RentEntity rent, CancellationToken cancellationToken)
    {
        await context.Rents.AddAsync(rent, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<RentEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.Rents.Where(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<RentEntity?> GetByMotoIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.Rents.Where(x => x.IdMoto == id).FirstOrDefaultAsync(cancellationToken);
    }
}