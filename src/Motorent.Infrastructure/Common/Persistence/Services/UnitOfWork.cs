using Microsoft.EntityFrameworkCore.Storage;
using Motorent.Application.Common.Abstractions.Persistence;

namespace Motorent.Infrastructure.Common.Persistence.Services;

internal sealed class UnitOfWork(DataContext dataContext) : IUnitOfWork
{
    private IDbContextTransaction? transaction;
    
    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default) => 
        transaction = await dataContext.Database.BeginTransactionAsync(cancellationToken);

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (transaction is not null)
        {
            await transaction.CommitAsync(cancellationToken);
        }
        
        transaction?.Dispose();
        transaction = null;
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (transaction is not null)
        {
            await transaction.RollbackAsync(cancellationToken);
        }
        
        transaction?.Dispose();
        transaction = null;
    }
}