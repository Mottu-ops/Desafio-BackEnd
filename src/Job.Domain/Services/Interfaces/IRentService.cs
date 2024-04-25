using Job.Domain.Commands;
using Job.Domain.Commands.Rent;

namespace Job.Domain.Services.Interfaces;

public interface IRentService
{
    Task<CommandResponse> CreateRentAsync(CreateRentCommand command, CancellationToken cancellationToken);

    Task<CommandResponse> CancelRentAsync(CancelRentCommand command, CancellationToken cancellationToken);
}