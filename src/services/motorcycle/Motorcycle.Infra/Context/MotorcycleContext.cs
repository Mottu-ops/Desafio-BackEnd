using Microsoft.EntityFrameworkCore;
using Motorcycle.Domain.Entities;
using Motorcycle.Infra.Mappings;

namespace Motorcycle.Infra.Context;

public class MotorcycleContext : DbContext
{
    public MotorcycleContext() { }

    public MotorcycleContext(DbContextOptions options) : base(options) { }

    public virtual DbSet<Vehicle> Motorcycles { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new MotorcycleMap());
    }

     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "Host=localhost;Username=postgres;Password=postgres;Database=SampleDbDriver; PORT=5433";
        optionsBuilder.UseNpgsql(connectionString);
    }
}