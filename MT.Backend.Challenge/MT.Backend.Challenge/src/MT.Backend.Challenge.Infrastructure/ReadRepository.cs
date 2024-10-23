using MT.Backend.Challenge.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using MT.Backend.Challenge.Domain.Entities;
using System.Reflection;

namespace MT.Backend.Challenge.Infrastructure
{
    public class ReadRepository<TEntity> : IReadRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext Context;
        public ReadRepository(
            AppDbContext context
            )
        {
            Context = context;
        }

        public async Task<OperationResult<List<TEntity>>> Find(Expression<Func<TEntity, bool>> predicate)
        {
            var operationResult = new OperationResult<List<TEntity>>();
            try
            {
                var data = await Context.Set<TEntity>().Where(predicate).ToListAsync();
                HandleSuccess(operationResult, data);
            }
            catch (Exception ex)
            {
                HandleException(operationResult, MethodBase.GetCurrentMethod().Name, ex);
            }
            return operationResult;
        }

        public async Task<OperationResult<TEntity?>> GetByIdAsync(string id)
        {
            var operationResult = new OperationResult<TEntity>();
            try
            {
                
                var    data = await Context.Set<TEntity>().FindAsync(id);
                
                HandleSuccess(operationResult, data);
            }
            catch (Exception ex)
            {
                HandleException(operationResult, MethodBase.GetCurrentMethod().Name, ex);
            }
            return operationResult;
        }

        private static void HandleException<T>(
            OperationResult<T> operation,
            string methodName,
            Exception ex
        )
        {
            operation.Success = false;
            operation.Message = $"An error occurred while processing the request on {methodName}.";
            operation.Exception = ex;

        }

        private static void HandleSuccess<T>(OperationResult<T> operation, T data)
        {
            operation.Success = true;
            operation.Result = data;
        }

    }
} 
