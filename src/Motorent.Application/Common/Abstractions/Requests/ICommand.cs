namespace Motorent.Application.Common.Abstractions.Requests;

public interface ICommand : IRequest<Result<Success>>;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>;