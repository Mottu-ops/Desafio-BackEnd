using Bogus;
using Job.Domain.Commands.Moto;

namespace Job.Commons.Domain.Commands.Moto;

public static class UpdateMotoCommandFaker
{
    public static Faker<UpdateMotoCommand> Default()
    {
        return new Faker<UpdateMotoCommand>()
            .CustomInstantiator(faker => new UpdateMotoCommand(
                faker.Random.Guid(),
                faker.Random.Int(1900, 2050),
                faker.Vehicle.Model(),
                "AAA5F55"
            ));
    }

    public static Faker<UpdateMotoCommand> Empty()
    {
        return new Faker<UpdateMotoCommand>()
            .CustomInstantiator(faker => new UpdateMotoCommand(
                Guid.Empty,
                0,
                string.Empty,
                string.Empty
            ));
    }

    public static Faker<UpdateMotoCommand> Invalid()
    {
        return new Faker<UpdateMotoCommand>()
            .CustomInstantiator(faker => new UpdateMotoCommand(
                Guid.Empty,
                0,
                string.Empty,
                faker.Random.AlphaNumeric(1)
            ));
    }
}