namespace Job.Domain.Entities.User;

public abstract class UserEntity(string password) : BaseEntity
{
    public string Password { get; private set; } = password;
}