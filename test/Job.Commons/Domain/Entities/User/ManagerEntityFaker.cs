using Bogus;
using Job.Domain.Entities.User;

namespace Job.Commons.Domain.Entities.User;

public static class ManagerEntityFaker
{
    public static Faker<ManagerEntity> Default()
    {
        return new Faker<ManagerEntity>()
            .CustomInstantiator(faker => new ManagerEntity(
                faker.Internet.Email(),
                faker.Internet.Password()
            ));
    }
}