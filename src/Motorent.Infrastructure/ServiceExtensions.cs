using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Motorent.Application.Common.Abstractions.Identity;
using Motorent.Application.Common.Abstractions.Persistence;
using Motorent.Application.Common.Abstractions.Security;
using Motorent.Domain.Renters.Repository;
using Motorent.Domain.Renters.Services;
using Motorent.Domain.Users.Repository;
using Motorent.Domain.Users.Services;
using Motorent.Infrastructure.Common.Identity;
using Motorent.Infrastructure.Common.Persistence;
using Motorent.Infrastructure.Common.Persistence.BackgroundJobs;
using Motorent.Infrastructure.Common.Persistence.Interceptors;
using Motorent.Infrastructure.Common.Security;
using Motorent.Infrastructure.Renters;
using Motorent.Infrastructure.Renters.Persistence;
using Motorent.Infrastructure.Users;
using Motorent.Infrastructure.Users.Persistence;
using Quartz;
using Serilog;

namespace Motorent.Infrastructure;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSerilog(config => config
            .ReadFrom.Configuration(configuration));

        services.AddAuthentication(configuration);

        services.AddAuthorization();

        services.AddPersistence(configuration);

        services.AddBackgroundJobs();

        services.AddHttpContextAccessor();

        services.AddTransient<IEncryptionService, EncryptionService>();
        services.AddTransient<IEmailUniquenessChecker, EmailUniquenessChecker>();

        services.AddTransient<ICnpjUniquenessChecker, CnpjUniquenessChecker>();
        services.AddTransient<ICnhUniquenessChecker, CnhUniquenessChecker>();
        services.AddTransient<IRenterFactory, RenterFactory>();

        services.AddTransient<TimeProvider>(_ => TimeProvider.System);
        services.AddTransient<ISecurityTokenService, SecurityTokenService>();

        services.AddScoped<IUserContext, UserContext>();

        return services;
    }

    private static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AuditEntitiesOnSaveChangesInterceptor>();
        services.AddScoped<PersistOutboxDomainEventsOnSaveChangesInterceptor>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddDbContext<DataContext>((provider, options) =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), pgsqlOptions =>
                pgsqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));

            options.AddInterceptors(
                provider.GetRequiredService<AuditEntitiesOnSaveChangesInterceptor>(),
                provider.GetRequiredService<PersistOutboxDomainEventsOnSaveChangesInterceptor>());
        });

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRenterRepository, RenterRepository>();
    }

    private static void AddBackgroundJobs(this IServiceCollection services)
    {
        services.AddQuartz(configurator =>
        {
            configurator.AddJob<ProcessOutboxMessagesJob>(builder => builder
                    .DisallowConcurrentExecution()
                    .WithIdentity(nameof(ProcessOutboxMessagesJob)))
                .AddTrigger(trigger => trigger
                    .ForJob(nameof(ProcessOutboxMessagesJob))
                    .WithSimpleSchedule(schedule => schedule
                        .WithInterval(TimeSpan.FromSeconds(10))
                        .RepeatForever()));
        });

        services.AddQuartzHostedService();
    }

    private static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        JsonWebTokenHandler.DefaultInboundClaimTypeMap.Clear();

        var securityTokenOptionsSection = configuration.GetSection(SecurityTokenOptions.SectionName);
        services.AddOptions<SecurityTokenOptions>()
            .Bind(securityTokenOptionsSection)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        var securityTokenOptions = securityTokenOptionsSection.Get<SecurityTokenOptions>()!;
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = securityTokenOptions.Issuer,
                    ValidAudience = securityTokenOptions.Audience,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(securityTokenOptions.Key))
                };
            });
    }
}