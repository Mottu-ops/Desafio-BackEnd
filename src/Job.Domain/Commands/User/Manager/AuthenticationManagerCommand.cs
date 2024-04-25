namespace Job.Domain.Commands.User.Manager;

public record AuthenticationManagerCommand(string email, string password);