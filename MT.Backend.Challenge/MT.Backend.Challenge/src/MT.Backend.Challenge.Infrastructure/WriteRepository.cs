using MT.Backend.Challenge.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MT.Backend.Challenge.Domain.Entities;
using System.Reflection;

namespace MT.Backend.Challenge.Infrastructure
{
    public class WriteRepository<TEntity> : IWriteRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext Context;
        public WriteRepository(
            AppDbContext context
            )
        {
            Context = context;
        }

        public async Task<OperationResult<bool>> Delete(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }
                Context.Set<TEntity>().Remove(entity);
                await Context.SaveChangesAsync();

                return HandleSuccess();
            }
            catch (Exception ex)
            {
                return HandleException(MethodBase.GetCurrentMethod().Name, ex);
            }

        }

        public async Task<OperationResult<bool>> Update(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }

                Context.Set<TEntity>().Update(entity);
                await Context.SaveChangesAsync();
                return HandleSuccess();
            }
            catch (Exception ex)
            {
                return HandleException(MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public async Task<OperationResult<bool>> Add(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }

                await Context.Set<TEntity>().AddAsync(entity);
                await Context.SaveChangesAsync();

                return HandleSuccess();
            }
            catch (Exception ex)
            {
                return HandleException(MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public async Task<OperationResult<bool>> Execute(string procedure)
        {
            try
            {
                if (string.IsNullOrEmpty(procedure))
                {
                    throw new ArgumentNullException(nameof(procedure));
                }

                await Context.Database.ExecuteSqlRawAsync(procedure);
                return HandleSuccess();
            }
            catch (Exception ex)
            {
                return HandleException(MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private static OperationResult<bool> HandleException(
          string methodName,
          Exception ex
        )
        {
            var operation = new OperationResult<bool>();
            operation.Success = false;
            operation.Message = $"An error occurred while processing the request on {methodName}.";
            operation.Exception = ex;

            return operation;
        }

        private static OperationResult<bool> HandleSuccess()
        {
            return new OperationResult<bool> { Success = true, Result = true };
        }


    }
}
