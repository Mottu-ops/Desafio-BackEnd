namespace Job.Domain.Commands;

public sealed record ErrorMessage(string Message, string Property, string? AttemptedValue = null);