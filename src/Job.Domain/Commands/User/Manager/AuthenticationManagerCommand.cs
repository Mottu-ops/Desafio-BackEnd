namespace Job.Domain.Commands.User.Manager;

public sealed record AuthenticationManagerCommand(string Email, string Password);