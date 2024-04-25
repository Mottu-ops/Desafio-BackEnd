using Job.Domain.Repositories;
using Job.Domain.Services;
using Job.Domain.Services.Interfaces;
using Job.Infrastructure.Context;
using Job.Infrastructure.Repositories;
using Job.WebApi.Services;
using Microsoft.EntityFrameworkCore;

namespace Job.WebApi.Infrastructure;

public static class DependencyInjectService
{
    public static void AddDependencyInject(this IServiceCollection services)
    {
        services.AddDbContext<JobContext>(options =>
        {
            options.UseNpgsql("Server=localhost;Port=5432;User Id=postgres;Password=postgres;Database=job;");
        });

        services.RegisterService();
        services.RegisterRepository();
        services.AddTransient<TokenService>();
    }

    private static void RegisterRepository(this IServiceCollection services)
    {
        services.AddScoped<IManagerRepository, ManagerRepository>();
        services.AddScoped<IMotoboyRepository, MotoboyRepository>();
        services.AddScoped<IRentRepository, RentRepository>();
        services.AddScoped<IMotoRepository, MotoRepository>();
    }

    private static void RegisterService(this IServiceCollection services)
    {
        services.AddScoped<IMotoboyService, MotoboyService>();
        services.AddScoped<IRentService, RentService>();
        services.AddScoped<IMotoService, MotoService>();
        services.AddScoped<IManagerService, ManagerService>();
    }
}