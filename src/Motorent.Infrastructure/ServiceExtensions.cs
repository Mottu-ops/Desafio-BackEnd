using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Motorent.Application.Common.Abstractions.Persistence;
using Motorent.Infrastructure.Common.Persistence;
using Motorent.Infrastructure.Common.Persistence.Services;
using Motorent.Infrastructure.Common.Security;
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
        
        return services;
    }
    
    private static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddDbContext<DataContext>((_, options) =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), pgsqlOptions =>
                pgsqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
        });
    }
    
    private static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
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