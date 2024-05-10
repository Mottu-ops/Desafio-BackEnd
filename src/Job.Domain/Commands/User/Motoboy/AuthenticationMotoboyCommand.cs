namespace Job.Domain.Commands.User.Motoboy;

public sealed record AuthenticationMotoboyCommand(string Cnpj, string Password);