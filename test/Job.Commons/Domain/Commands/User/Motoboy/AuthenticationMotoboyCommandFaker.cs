using Bogus;
using Bogus.Extensions.Brazil;
using Job.Domain.Commands.User.Motoboy;

namespace Job.Commons.Domain.Commands.User.Motoboy;

public static class AuthenticationMotoboyCommandFaker
{
    public static Faker<AuthenticationMotoboyCommand> Default()
    {
        return new Faker<AuthenticationMotoboyCommand>()
            .CustomInstantiator(faker => new AuthenticationMotoboyCommand(
                faker.Company.Cnpj(),
                faker.Internet.Password()
            ));
    }

    public static Faker<AuthenticationMotoboyCommand> Invalid()
    {
        return new Faker<AuthenticationMotoboyCommand>()
            .CustomInstantiator(faker => new AuthenticationMotoboyCommand(
                faker.Random.AlphaNumeric(5),
                faker.Lorem.Letter(5)
            ));
    }
}