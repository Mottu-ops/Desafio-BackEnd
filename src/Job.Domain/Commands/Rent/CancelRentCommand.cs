namespace Job.Domain.Commands.Rent;

public sealed record CancelRentCommand(Guid Id, DateTime DatePreview);