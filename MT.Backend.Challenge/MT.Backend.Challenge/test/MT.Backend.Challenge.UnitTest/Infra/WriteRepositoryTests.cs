using Bogus;
using Microsoft.EntityFrameworkCore;
using MT.Backend.Challenge.Domain.Entities;
using MT.Backend.Challenge.Infrastructure;

namespace MT.Backend.Challenge.UnitTest.Infra
{
    public class WriteRepositoryTests
    {
        private readonly WriteRepository<BaseEntity> Repository;
        private readonly AppDbContext DbContext;
        private readonly Faker Faker = new Faker();

        public WriteRepositoryTests()
        {
            // Configura um DbContext com InMemoryDatabase para testes
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Banco em memória
                .Options;

            DbContext = new AppDbContext(options);

            // Cria uma instância do repositório usando o contexto configurado
            Repository = new WriteRepository<BaseEntity>(DbContext);
        }

        [Fact]
        public async Task Add_ShouldInsertEntity_WhenEntityIsValid()
        {
            // Arrange
            var entity = new DeliveryDriver
            {
                Id = Faker.Random.Guid().ToString(),
                Name = Faker.Name.FullName(),
                Document = Faker.Random.String2(10)
            };

            // Act
            var result = await Repository.Add(entity);
            var insertedEntity = await DbContext.Set<DeliveryDriver>().FindAsync(entity.Id);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(insertedEntity);
            Assert.Equal(entity.Id, insertedEntity.Id);
        }

        [Fact]
        public async Task Update_ShouldModifyEntity_WhenEntityExists()
        {
            // Arrange
            var entity = new DeliveryDriver
            {
                Id = Faker.Random.Guid().ToString(),
                Name = "Initial Name",
                Document = Faker.Random.String2(10)
            };

            await DbContext.AddAsync(entity);
            await DbContext.SaveChangesAsync();

            // Modify the entity
            entity.Name = "Updated Name";

            // Act
            var result = await Repository.Update(entity);
            var updatedEntity = await DbContext.Set<DeliveryDriver>().FindAsync(entity.Id);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(updatedEntity);
            Assert.Equal("Updated Name", updatedEntity.Name);
        }

        [Fact]
        public async Task Delete_ShouldRemoveEntity_WhenEntityExists()
        {
            // Arrange
            var entity = new DeliveryDriver
            {
                Id = Faker.Random.Guid().ToString(),
                Name = Faker.Name.FullName(),
                Document = Faker.Random.String2(10)
            };

            await DbContext.AddAsync(entity);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await Repository.Delete(entity);
            var deletedEntity = await DbContext.Set<DeliveryDriver>().FindAsync(entity.Id);

            // Assert
            Assert.True(result.Success);
            Assert.Null(deletedEntity);
        }

        [Fact]
        public async Task Delete_ShouldReturnFailure_WhenEntityDoesNotExist()
        {
            // Arrange
            var nonExistentEntity = new DeliveryDriver
            {
                Id = Faker.Random.Guid().ToString(),
                Name = Faker.Name.FullName()
            };

            // Act
            var result = await Repository.Delete(nonExistentEntity);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task Add_ShouldHandleDatabaseException()
        {
            // Arrange
            var repositoryMock = new WriteRepository<BaseEntity>(DbContext);

            // Forçar uma exceção inserindo uma entidade nula
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await repositoryMock.Add(null);
            });
        }
    }
}
