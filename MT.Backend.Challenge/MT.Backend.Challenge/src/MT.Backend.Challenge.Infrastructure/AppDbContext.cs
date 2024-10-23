using MT.Backend.Challenge.Domain.Entities;
using MT.Backend.Challenge.Infrastructure.ContextHelpers;
using Microsoft.EntityFrameworkCore;

namespace MT.Backend.Challenge.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // dentro de .\src\MT.Backend.Challenge.api
        // dotnet ef migrations add InitialMT.Backend.Challenge.Migrations -p ..\MT.Backend.Challenge.Infrastructure
        // dotnet ef database update 

        public DbSet<RentalCategory> RentalCategories { get; set; }
        public DbSet<Motorcycle> Motorcycles { get; set; }
        public DbSet<DeliveryDriver> DeliveryDrivers { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração para DateTime converter para UTC
            ConfigEntities(modelBuilder);

            // Configuração de tipos de dados genéricos
            ConfigureDataTypes.ConfigureGenericDataTypes(modelBuilder);

            // Inserção de dados iniciais
            InsertDataValues.InsertDataRentalCategoty(modelBuilder);
        }

        private static void ConfigEntities(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                    {
                        property.SetValueConverter(
                            new Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<DateTime, DateTime>(
                                v => v.ToUniversalTime(),
                                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)));
                    }
                }
            }

            // Configuração para entidades que herdam de BaseEntity
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    // Configura o campo 'Id' como chave primária
                    modelBuilder.Entity(entityType.ClrType).HasKey("Id");

                    // Configura o campo 'Id' como único e com tamanho máximo de 50
                    modelBuilder.Entity(entityType.ClrType)
                        .Property("Id")
                        .HasMaxLength(50)
                        .IsRequired();

                    modelBuilder.Entity(entityType.ClrType)
                        .HasIndex("Id")
                        .IsUnique();
                }
            }
        }
    }
}
