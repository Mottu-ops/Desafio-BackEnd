using FluentValidation.Results;

namespace Job.Domain.Commands;

public sealed class CommandResponse<T>
{
    public CommandResponse(IEnumerable<ValidationFailure> failure)
    {
        Errors = failure.Select(x =>
            new ErrorMessage(
                x.ErrorMessage,
                x.PropertyName,
                x.AttemptedValue?.ToString()));
        Id = Guid.Empty;
        Data = default!;
    }

    public CommandResponse(Guid id)
    {
        Errors = new List<ErrorMessage>();
        Id = id;
        Data = default!;
    }

    public CommandResponse()
    {
        Errors = new List<ErrorMessage>();
        Id = Guid.Empty;
        Data = default!;
    }

    public Guid Id { get; }

    public IEnumerable<ErrorMessage> Errors { get; }

    public T Data { get; set; }

    public bool Success => Errors.Any() is false;
}