using Job.Domain.Commands;
using Job.Domain.Commands.Moto;
using Job.Domain.Queries.Moto;

namespace Job.Domain.Services.Interfaces;

public interface IMotoService
{
    Task<CommandResponse> CreateAsync(CreateMotoCommand command, CancellationToken cancellationToken);
    Task<CommandResponse> UpdateAsync(UpdateMotoCommand command, CancellationToken cancellationToken);
    Task<CommandResponse> DeleteAsync(Guid idMoto, CancellationToken cancellationToken);
    Task<IEnumerable<MotoQuery>> GetAllAsync(CancellationToken cancellationToken);
    Task<MotoQuery?> GetByIdAsync(Guid idMoto, CancellationToken cancellationToken);
}