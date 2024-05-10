using Job.Domain.Enums;

namespace Job.Domain.Commands.User.Motoboy;

public sealed record CreateMotoboyCommand(string Name, string Password, string Cnpj, DateTime DateBirth, string Cnh, ECnhType TypeCnh);