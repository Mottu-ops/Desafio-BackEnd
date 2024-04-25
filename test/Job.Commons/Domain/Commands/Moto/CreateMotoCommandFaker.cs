using Bogus;
using Job.Domain.Commands.Moto;

namespace Job.Commons.Domain.Commands.Moto;

public static class CreateMotoCommandFaker
{
    public static Faker<CreateMotoCommand> Default()
    {
        return new Faker<CreateMotoCommand>()
            .CustomInstantiator(faker => new CreateMotoCommand(
                faker.Random.Int(1900, 2050),
                faker.Vehicle.Model(),
                faker.Vehicle.Vin()
            ));
    }

    public static Faker<CreateMotoCommand> Empty()
    {
        return new Faker<CreateMotoCommand>()
            .CustomInstantiator(faker => new CreateMotoCommand(
                0,
                string.Empty,
                string.Empty
            ));
    }
}