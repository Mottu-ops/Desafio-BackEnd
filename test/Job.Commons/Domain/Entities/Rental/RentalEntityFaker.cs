using Bogus;
using Job.Domain.Entities.Rental;
using Job.Domain.Enums;

namespace Job.Commons.Domain.Entities.Rental;

public static class RentalEntityFaker
{
    public static Faker<RentalEntity> Default()
    {
        return new Faker<RentalEntity>()
            .CustomInstantiator(faker => new RentalEntity(
                faker.Random.Guid(),
                faker.Random.Guid(),
                faker.Date.FutureDateOnly(),
                faker.PickRandom<EPlan>()
            ));
    }

    public static Faker<RentalEntity> Invalid()
    {
        return new Faker<RentalEntity>()
            .CustomInstantiator(faker => new RentalEntity(
                Guid.Empty,
                Guid.Empty,
                faker.Date.PastDateOnly(),
                faker.PickRandom<EPlan>()
            ));
    }
}