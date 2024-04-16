using Microsoft.Extensions.Logging;
using Motorent.Application.Common.Abstractions.Persistence;
using Motorent.Application.Common.Abstractions.Requests;

namespace Motorent.Application.Common.Behaviors;

internal sealed class TransactionBehavior<TRequest, TResponse>(
    IUnitOfWork unitOfWork,
    ILogger<TransactionBehavior<TRequest, TResponse>> logger) 
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>, ITransactional
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        
        try
        {
            logger.LogInformation("Starting transaction for {RequestName}", requestName);
            await unitOfWork.BeginTransactionAsync(cancellationToken);

            var response = await next();

            logger.LogInformation("Committing transaction for {RequestName}", requestName);
            await unitOfWork.CommitTransactionAsync(cancellationToken);

            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Rolling back transaction for {RequestName}", requestName);
            await unitOfWork.RollbackTransactionAsync(cancellationToken);

            throw;
        }
    }
}