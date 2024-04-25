using Microsoft.EntityFrameworkCore;
using Rent.Domain.Entities;
using Rent.Infra.Mappings;

namespace Rent.Infra.Context;
public class TransactionContext : DbContext {
    public TransactionContext() {}

    public TransactionContext(DbContextOptions options) : base(options) { }

    public virtual DbSet<Transaction> Motorcycles { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new TransactionMap());
    }

     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "Host=localhost;Username=postgres;Password=postgres;Database=SampleDbDriver; PORT=5433";
        optionsBuilder.UseNpgsql(connectionString);
    }
}