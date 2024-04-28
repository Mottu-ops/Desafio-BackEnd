using Bogus;
using Job.Domain.Commands.Rent;

namespace Job.Commons.Domain.Commands.Rent;

public static class CancelRentCommandFaker
{
    public static Faker<CancelRentCommand> Default()
    {
        return new Faker<CancelRentCommand>()
            .CustomInstantiator(faker => new CancelRentCommand(
                faker.Random.Guid(),
                faker.Date.Future()
            ));
    }

    public static Faker<CancelRentCommand> Empty()
    {
        return new Faker<CancelRentCommand>()
            .CustomInstantiator(faker => new CancelRentCommand(
                Guid.Empty,
                DateTime.MinValue
            ));
    }

    public static Faker<CancelRentCommand> Invalid()
    {
        return new Faker<CancelRentCommand>()
            .CustomInstantiator(faker => new CancelRentCommand(
                Guid.Empty,
                faker.Date.Past()
            ));
    }
}