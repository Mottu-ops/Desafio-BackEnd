using Job.Domain.Commons;
using Job.Domain.Enums;

namespace Job.Domain.Entities.User;

public sealed class MotoboyEntity(string password, string name, string cnpj, DateOnly dateBirth, string cnh, ECnhType type)
    : UserEntity(password)
{
    public string Name { get; private set; } = name;
    public string Cnpj { get; private set; } = CnpjValidation.FormatCnpj(cnpj);

    public DateOnly DateBirth { get; private set; } = dateBirth;

    public string Cnh { get; private set; } = CnhValidation.FormatCnh(cnh);
    public ECnhType Type { get; private set; } = type;

    public string? CnhImage { get; private set; } = string.Empty;

    public void UpdateCnhImage(string image)
    {
        Update();
        CnhImage = image;
    }

}