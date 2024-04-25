namespace Job.Domain.Commands;

public record ErrorMessage(string Message, string Property, string? AttemptedValue = null);