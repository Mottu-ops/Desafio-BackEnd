using Microsoft.EntityFrameworkCore;
using Order.Domain.Entities;
using Order.Infra.Mappings;

namespace Order.Infra.Context;

public class OrderContext : DbContext
{
    public OrderContext() { }

    public OrderContext(DbContextOptions options) : base(options) { }

    public virtual DbSet<OrderEntity> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new OrderMap());
    }

     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "Host=localhost;Username=postgres;Password=postgres;Database=SampleDbDriver; PORT=5433";
        optionsBuilder.UseNpgsql(connectionString);
    }
}