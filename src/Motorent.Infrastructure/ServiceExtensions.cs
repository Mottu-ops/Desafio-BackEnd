using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Motorent.Infrastructure.Common.Persistence;
using Serilog;

namespace Motorent.Infrastructure;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSerilog(config => config
            .ReadFrom.Configuration(configuration));
        
        services.AddAuthentication();
        services.AddAuthorization();

        services.AddPersistence(configuration);
        
        return services;
    }
    
    private static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>((_, options) =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), pgsqlOptions =>
                pgsqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
        });
    }
}