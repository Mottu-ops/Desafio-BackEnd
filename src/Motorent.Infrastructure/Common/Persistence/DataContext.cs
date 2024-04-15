using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Motorent.Infrastructure.Common.Persistence;

internal sealed class DataContext(DbContextOptions options) : DbContext(options)
{
    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        base.ConfigureConventions(builder);

        builder.Properties<Uri>()
            .HaveConversion<UriToStringConverter>();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        base.OnConfiguring(builder);
        
        builder.UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }
}