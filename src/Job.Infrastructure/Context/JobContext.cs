using Job.Domain.Entities.Moto;
using Job.Domain.Entities.Rent;
using Job.Domain.Entities.User;
using Job.Infrastructure.Conversions;
using Microsoft.EntityFrameworkCore;
using DateOnlyConverter = Job.Infrastructure.Conversions.DateOnlyConverter;

namespace Job.Infrastructure.Context;

public sealed class JobContext : DbContext
{
    public JobContext(DbContextOptions<JobContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        configurationBuilder.Properties<DateOnly>()
            .HaveConversion<DateOnlyConverter, DateOnlyComparer>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(JobContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<RentEntity> Rents => Set<RentEntity>();
    public DbSet<MotoboyEntity> Motoboys => Set<MotoboyEntity>();
    public DbSet<MotoEntity> Motos => Set<MotoEntity>();
    public DbSet<ManagerEntity> Managers => Set<ManagerEntity>();
}