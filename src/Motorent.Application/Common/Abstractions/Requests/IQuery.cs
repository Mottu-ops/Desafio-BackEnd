namespace Motorent.Application.Common.Abstractions.Requests;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;