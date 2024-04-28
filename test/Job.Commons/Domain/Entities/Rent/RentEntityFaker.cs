using Bogus;
using Job.Domain.Entities.Rent;
using Job.Domain.Enums;

namespace Job.Commons.Domain.Entities.Rent;

public static class RentEntityFaker
{
    public static Faker<RentEntity> Default()
    {
        return new Faker<RentEntity>()
            .CustomInstantiator(faker => new RentEntity(
                faker.Random.Guid(),
                faker.Random.Guid(),
                faker.Date.FutureDateOnly(),
                faker.PickRandom<EPlan>()
            ));
    }

    public static Faker<RentEntity> Invalid()
    {
        return new Faker<RentEntity>()
            .CustomInstantiator(faker => new RentEntity(
                Guid.Empty,
                Guid.Empty,
                faker.Date.PastDateOnly(),
                faker.PickRandom<EPlan>()
            ));
    }
}