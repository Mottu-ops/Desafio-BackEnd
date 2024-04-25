namespace Job.Domain.Entities.User;

public sealed class ManagerEntity(string email, string password) : UserEntity(password)
{
    public string Email { get; private set; } = email;
}