using Job.Domain.Entities.User;

namespace Job.Domain.Repositories;

public interface IManagerRepository
{
    Task<ManagerEntity?> GetAsync(string email, string password, CancellationToken cancellationToken = default);
}