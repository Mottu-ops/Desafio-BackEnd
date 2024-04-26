using Job.Domain.Commands;
using Job.Domain.Commands.User.Motoboy;
using Job.Domain.Queries.User;

namespace Job.Domain.Services.Interfaces;

public interface IMotoboyService
{
    Task<CommandResponse<string>> CreateAsync(CreateMotoboyCommand command, CancellationToken cancellationToken);

    Task<MotoboyQuery?> GetMotoboy(AuthenticationMotoboyCommand command, CancellationToken cancellationToken);

    Task<CommandResponse<string>> UploadImageAsync(string cnpj, UploadCnhMotoboyCommand file, CancellationToken cancellationToken);
}