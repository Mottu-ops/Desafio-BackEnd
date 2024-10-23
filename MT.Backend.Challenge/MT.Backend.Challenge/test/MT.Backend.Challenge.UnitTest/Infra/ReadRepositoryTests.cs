using Bogus;
using Bogus.Extensions.Brazil;
using Microsoft.EntityFrameworkCore;
using Moq;
using MT.Backend.Challenge.Domain.Entities;
using MT.Backend.Challenge.Infrastructure;

namespace MT.Backend.Challenge.UnitTest.Infra
{
    public class ReadRepositoryTests
    {
        private readonly ReadRepository<BaseEntity> Repository;
        private readonly AppDbContext DbContext;
        private readonly Faker Faker = new Faker();

        public ReadRepositoryTests()
        {
            // Configura um DbContext com InMemoryDatabase para testes
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Banco em memória
                .Options;

            DbContext = new AppDbContext(options);

            // Cria uma instância do repositório usando o contexto configurado
            Repository = new ReadRepository<BaseEntity>(DbContext);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnEntity_WhenEntityExists()
        {
            // Arrange
            var entity = new DeliveryDriver { Id = Faker.Random.Guid().ToString(), Name = "Test Driver" };
            DbContext.Add(entity);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await Repository.GetByIdAsync(entity.Id);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Result);
            Assert.Equal(entity.Id, result.Result.Id);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenEntityDoesNotExist()
        {
            // Arrange
            var invalidId = Faker.Random.Guid().ToString();

            // Act
            var result = await Repository.GetByIdAsync(invalidId);

            // Assert
            Assert.True(result.Success);
            Assert.Null(result.Result);
        }

        [Fact]
        public async Task FindAll_ShouldReturnAllEntities_WhenEntitiesExist()
        {
            var document = Faker.Company.Cnpj();
            // Arrange
            var entities = Faker.Make(5, () => new DeliveryDriver
            {
                Id = Faker.Random.Guid().ToString(),
                Name = Faker.Name.FullName(),
                Document = Faker.Company.Cnpj()
            });

            entities[2].Document = document;

            DbContext.AddRange(entities);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await Repository.Find(x => ((DeliveryDriver)x).Document == document);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(5, result.Result.Count);
        }

        [Fact]
        public async Task FindAll_ShouldReturnEmptyList_WhenNoEntitiesExist()
        {
            var document = Faker.Company.Cnpj();
            // Act
            var result = await Repository.Find(x => ((DeliveryDriver)x).Document == document);

            // Assert
            Assert.True(result.Success);
            Assert.Empty(result.Result);
        }

        [Fact]
        public async Task Find_ShouldReturnMatchingEntities_WhenFilterMatches()
        {
            // Arrange
            var matchingEntity = new DeliveryDriver
            {
                Id = Faker.Random.Guid().ToString(),
                Name = "Specific Name"
            };

            var otherEntity = new DeliveryDriver
            {
                Id = Faker.Random.Guid().ToString(),
                Name = Faker.Name.FullName()
            };

            DbContext.AddRange(matchingEntity, otherEntity);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await Repository.Find(x => ((DeliveryDriver)x).Name == "Specific Name");

            // Assert
            Assert.True(result.Success);
            Assert.Single(result.Result);
            Assert.Equal(matchingEntity.Id, result.Result.First().Id);
        }

        [Fact]
        public async Task Find_ShouldReturnEmptyList_WhenNoMatchFound()
        {
            // Arrange
            var entity = new DeliveryDriver
            {
                Id = Faker.Random.Guid().ToString(),
                Name = Faker.Name.FullName()
            };

            DbContext.Add(entity);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await Repository.Find(x => ((DeliveryDriver)x).Name == "Nonexistent Name");

            // Assert
            Assert.True(result.Success);
            Assert.Empty(result.Result);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldHandleDatabaseException()
        {
            // Arrange
            var repository = new Mock<ReadRepository<BaseEntity>>(DbContext);
            repository.Setup(r => r.GetByIdAsync(It.IsAny<string>()))
                      .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await repository.Object.GetByIdAsync("invalid-id");

            // Assert
            Assert.False(result.Success);
            Assert.NotNull(result.Exception);
            Assert.Equal("Database error", result.Exception.Message);
        }
    }
}
