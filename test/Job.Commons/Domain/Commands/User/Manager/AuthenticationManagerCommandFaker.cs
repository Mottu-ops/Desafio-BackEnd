using Bogus;
using Job.Domain.Commands.User.Manager;

namespace Job.Commons.Domain.Commands.User.Manager;

public class AuthenticationManagerCommandFaker
{
    public static Faker<AuthenticationManagerCommand> Default()
    {
        return new Faker<AuthenticationManagerCommand>()
            .CustomInstantiator(faker => new AuthenticationManagerCommand(
                faker.Internet.Email(),
                faker.Internet.Password()
            ));
    }

    public static Faker<AuthenticationManagerCommand> Invalid()
    {
        return new Faker<AuthenticationManagerCommand>()
            .CustomInstantiator(faker => new AuthenticationManagerCommand(
                faker.Internet.Email(),
                faker.Lorem.Letter(5)
            ));
    }
}