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
                DateOnly.FromDateTime(DateTime.Now.AddDays(5)),
                EPlan.Sete
            ));
    }
}