namespace Job.Domain.Commands.User.Manager;

public record AuthenticationManagerCommand(string Email, string Password);