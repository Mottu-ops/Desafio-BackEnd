using Job.Domain.Entities.Rental;

namespace Job.Infrastructure.Repositories;

public class RentalRepository(JobContext context) : IRentalRepository
{
    public async Task CreateAsync(RentalEntity rental, CancellationToken cancellationToken)
    {
        await context.Rents.AddAsync(rental, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<RentalEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.Rents.Where(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<RentalEntity?> GetByMotoIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.Rents.Where(x => x.IdMoto == id).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task UpdateAsync(RentalEntity rental, CancellationToken cancellationToken)
    {
        context.Rents.Update(rental);
        await context.SaveChangesAsync(cancellationToken);
    }
}