using FluentValidation.Results;

namespace Job.Domain.Commands;

public sealed class CommandResponse<T>
{
    public CommandResponse(IEnumerable<ValidationFailure> failure)
    {
        Message = failure.Select(x =>
            new ErrorMessage(
                x.ErrorMessage,
                x.PropertyName,
                x.AttemptedValue?.ToString()));
        Id = Guid.Empty;
        Data = default!;
    }

    public CommandResponse(Guid id)
    {
        Message = new List<ErrorMessage>();
        Id = id;
        Data = default!;
    }

    public Guid Id { get; }

    public IEnumerable<ErrorMessage> Message { get; }

    public T Data { get; set; }

    public bool Success => Message.Any() is false;
}