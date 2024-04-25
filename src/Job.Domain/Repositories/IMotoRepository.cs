using Job.Domain.Entities.Moto;

namespace Job.Domain.Repositories;

public interface IMotoRepository
{
    Task CreateAsync(MotoEntity moto, CancellationToken cancellationToken);
    Task UpdateAsync(MotoEntity moto, CancellationToken cancellationToken);

    Task DeleteAsync(MotoEntity moto, CancellationToken cancellationToken);
    Task<MotoEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<MotoEntity?> GetByPlateAsync(string plate, CancellationToken cancellationToken);

    Task<IEnumerable<MotoEntity>> GetAllAsync(CancellationToken cancellationToken);
    Task<bool> CheckPlateExistsAsync(string plate, CancellationToken cancellationToken);
}