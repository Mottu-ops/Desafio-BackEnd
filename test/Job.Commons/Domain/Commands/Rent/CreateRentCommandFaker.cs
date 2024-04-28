using Bogus;
using Bogus.Extensions.Brazil;
using Job.Domain.Commands.Rent;
using Job.Domain.Enums;

namespace Job.Commons.Domain.Commands.Rent;

public static class CreateRentCommandFaker
{
    public static Faker<CreateRentCommand> Default()
    {
        return new Faker<CreateRentCommand>()
            .CustomInstantiator(faker => new CreateRentCommand(
                faker.Random.Guid(),
                faker.Date.Future(),
                faker.PickRandom<EPlan>()
            )
            {
                Cnpj = faker.Company.Cnpj()
            });
    }

    public static Faker<CreateRentCommand> Empty()
    {
        return new Faker<CreateRentCommand>()
            .CustomInstantiator(_ => new CreateRentCommand(
                Guid.Empty,
                DateTime.MinValue,
                0
            ));
    }

    public static Faker<CreateRentCommand> Invalid()
    {
        return new Faker<CreateRentCommand>()
            .CustomInstantiator(faker => new CreateRentCommand(
                Guid.Empty,
                faker.Date.Past(),
                0
            ));
    }
}