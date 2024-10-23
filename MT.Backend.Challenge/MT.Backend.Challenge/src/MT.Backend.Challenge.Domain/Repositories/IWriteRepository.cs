using MT.Backend.Challenge.Domain.Entities;
using System.Threading.Tasks;

namespace MT.Backend.Challenge.Domain.Repositories
{
    public interface IWriteRepository<TEntity> where TEntity : class
    {
        Task<OperationResult<bool>> Add(TEntity entity);
        Task<OperationResult<bool>> Update(TEntity entity);
        Task<OperationResult<bool>> Delete(TEntity entity);
        Task<OperationResult<bool>> Execute(string procedure);

    }
}
