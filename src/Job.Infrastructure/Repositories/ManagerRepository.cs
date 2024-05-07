using Job.Domain.Entities.User;

namespace Job.Infrastructure.Repositories;

public class ManagerRepository(JobContext context) : IManagerRepository
{
    public async Task<ManagerEntity?> GetAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        return await context.Managers.Where(x => x.Email == email && x.Password == password).FirstOrDefaultAsync(cancellationToken);
    }
}