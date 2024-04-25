using Job.Domain.Commands;
using Job.Domain.Commands.User.Motoboy;
using Job.Domain.Queries.User;
using Microsoft.AspNetCore.Http;

namespace Job.Domain.Services.Interfaces;

public interface IMotoboyService
{
    Task<CommandResponse> CreateAsync(CreateMotoboyCommand command, CancellationToken cancellationToken);

    Task<MotoboyQuery?> GetMotoboy(AuthenticationMotoboyCommand command, CancellationToken cancellationToken);

    Task<CommandResponse> UploadImageAsync(string cnpj, UploadCnhMotoboyCommand file, CancellationToken cancellationToken);
}