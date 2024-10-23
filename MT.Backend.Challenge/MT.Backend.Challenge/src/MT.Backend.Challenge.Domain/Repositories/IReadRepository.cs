using MT.Backend.Challenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MT.Backend.Challenge.Domain.Repositories
{
    public interface IReadRepository<TEntity> where TEntity : class
    {
        Task<OperationResult<TEntity?>> GetByIdAsync(string id);

        Task<OperationResult<List<TEntity>>> Find(Expression<Func<TEntity, bool>> predicate);

    }

}
