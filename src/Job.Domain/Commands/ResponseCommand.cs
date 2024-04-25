using FluentValidation.Results;

namespace Job.Domain.Commands;

public sealed class CommandResponse
{
    public CommandResponse(IEnumerable<ValidationFailure> failure)
    {
        Message = failure.Select(x =>
            new ErrorMessage(
                x.ErrorMessage,
                x.PropertyName,
                x.AttemptedValue?.ToString()));
        Id = Guid.Empty;
    }

    public CommandResponse(Guid id)
    {
        Message = new List<ErrorMessage>();
        Id = id;
    }

    public Guid Id { get; }

    public IEnumerable<ErrorMessage> Message { get; }

    public bool Success => Message.Any() is false;
}