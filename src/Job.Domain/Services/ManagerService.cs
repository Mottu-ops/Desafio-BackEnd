using Job.Domain.Commands.User.Manager;
using Job.Domain.Queries.User;
using Job.Domain.Repositories;
using Job.Domain.Services.Interfaces;

namespace Job.Domain.Services;

public class ManagerService(
    ILogger<ManagerService> logger,
    IManagerRepository managerRepository) : IManagerService
{
    public async Task<ManagerQuery?> GetManager(AuthenticationManagerCommand command, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Buscando admin {email}", command.email);
        var manager = await managerRepository.GetAsync(command.email, command.password, cancellationToken);

        if (manager is null)
        {
            logger.LogError("Manager not found");
            return null;
        }

        logger.LogInformation("Admin encontrado com sucesso");
        return new ManagerQuery(manager.Id, manager.Email);
    }
}