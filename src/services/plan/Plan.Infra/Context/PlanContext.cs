using Microsoft.EntityFrameworkCore;
using Plan.Domain.Entity;
using Plan.Infra.Mappings;

namespace Plan.Infra.Context;

public class PlanContext : DbContext {
    public PlanContext() {}
    public PlanContext(DbContextOptions options) {}

    public virtual DbSet<RentPlan> Plans { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new PlanMap());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "Host=localhost;Username=postgres;Password=postgres;Database=SampleDbDriver; PORT=5433";
        optionsBuilder.UseNpgsql(connectionString);
    }
}