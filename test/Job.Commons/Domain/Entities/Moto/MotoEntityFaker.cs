using Bogus;
using Job.Domain.Entities.Moto;

namespace Job.Commons.Domain.Entities.Moto;

public class MotoEntityFaker
{
    public static Faker<MotoEntity> Default()
    {
        return new Faker<MotoEntity>()
            .CustomInstantiator(faker => new MotoEntity(
                faker.Random.Int(1900, 2050),
                faker.Vehicle.Model(),
                faker.Vehicle.Vin()
            ));
    }
}