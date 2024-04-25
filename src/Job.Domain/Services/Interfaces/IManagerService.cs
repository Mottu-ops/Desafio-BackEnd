using Job.Domain.Commands.User.Manager;
using Job.Domain.Queries.User;

namespace Job.Domain.Services.Interfaces;

public interface IManagerService
{
    Task<ManagerQuery?> GetManager(AuthenticationManagerCommand command, CancellationToken cancellationToken = default);
}