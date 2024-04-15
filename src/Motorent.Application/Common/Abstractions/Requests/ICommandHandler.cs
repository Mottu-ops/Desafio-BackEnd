namespace Motorent.Application.Common.Abstractions.Requests;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Result<Success>>
    where TCommand : ICommand;
    
public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>;