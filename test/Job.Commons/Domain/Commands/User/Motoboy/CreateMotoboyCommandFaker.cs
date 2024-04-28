using Bogus;
using Bogus.Extensions.Brazil;
using Job.Domain.Commands.User.Motoboy;
using Job.Domain.Enums;

namespace Job.Commons.Domain.Commands.User.Motoboy;

public static class CreateMotoboyCommandFaker
{
    public static Faker<CreateMotoboyCommand> Default()
    {
        return new Faker<CreateMotoboyCommand>()
            .CustomInstantiator(faker => new CreateMotoboyCommand(
                faker.Person.FullName,
                faker.Internet.Password(),
                faker.Company.Cnpj(),
                faker.Person.DateOfBirth,
                "77058710884",
                faker.PickRandom<ECnhType>()
            ));
    }

    public static Faker<CreateMotoboyCommand> Invalid()
    {
        return new Faker<CreateMotoboyCommand>()
            .CustomInstantiator(faker => new CreateMotoboyCommand(
                string.Empty,
                faker.Lorem.Letter(5),
                faker.Random.AlphaNumeric(5),
                DateTime.Now,
                faker.Random.AlphaNumeric(5),
                faker.PickRandom<ECnhType>()
            ));
    }
}