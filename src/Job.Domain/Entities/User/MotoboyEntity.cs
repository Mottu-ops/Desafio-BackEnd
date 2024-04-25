using Job.Domain.Enums;

namespace Job.Domain.Entities.User;

public sealed class MotoboyEntity(string password, string name, string cnpj, DateOnly dateBirth, string document, ECnhType type)
    : UserEntity(password)
{
    public string Name { get; private set; } = name;
    public string Cnpj { get; private set; } = cnpj;

    public DateOnly DateBirth { get; private set; } = dateBirth;

    public string Document { get; private set; } = document;
    public ECnhType Type { get; private set; } = type;

    public string? CnhImage { get; private set; } = string.Empty;

    public void UpdateCnhImage(string image)
    {
        Update();
        CnhImage = image;
    }
}