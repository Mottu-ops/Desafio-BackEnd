using Job.Domain.Commands;
using Job.Domain.Commands.User.Manager;
using Job.Domain.Queries.User;
using Job.Domain.Repositories;
using Job.Domain.Services.Interfaces;

namespace Job.Domain.Services;

public class ManagerService(
    ILogger<ManagerService> logger,
    IManagerRepository managerRepository,
    IValidator<AuthenticationManagerCommand> validator) : IManagerService
{
    public async Task<CommandResponse<ManagerQuery?>> GetManager(AuthenticationManagerCommand command, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Buscando admin {email}", command.Email);
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if(validationResult.IsValid is false)
        {
            logger.LogError("Comando inválido");
            return new CommandResponse<ManagerQuery?>(validationResult.Errors);
        }

        var manager = await managerRepository.GetAsync(command.Email, command.Password, cancellationToken);
        if (manager is null)
        {
            logger.LogError("Manager not found");
            return new CommandResponse<ManagerQuery?>();
        }

        logger.LogInformation("Admin encontrado com sucesso");
        var query = new ManagerQuery(manager.Id, manager.Email);
        return new CommandResponse<ManagerQuery?>(query.Id)
        {
            Data = query
        };
    }
}