using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Motorent.Application.Common.Abstractions.Identity;
using Motorent.Application.Common.Abstractions.Persistence;
using Motorent.Application.Common.Abstractions.Security;
using Motorent.Domain.Users.Repository;
using Motorent.Domain.Users.Services;
using Motorent.Infrastructure.Common.Identity;
using Motorent.Infrastructure.Common.Persistence;
using Motorent.Infrastructure.Common.Security;
using Motorent.Infrastructure.Users;
using Motorent.Infrastructure.Users.Persistence;
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

        services.AddHttpContextAccessor();

        services.AddTransient<IEncryptionService, EncryptionService>();
        services.AddTransient<IEmailUniquenessChecker, EmailUniquenessChecker>();
        
        services.AddTransient<TimeProvider>(_ => TimeProvider.System);
        services.AddTransient<ISecurityTokenService, SecurityTokenService>();
        
        services.AddScoped<IUserContext, UserContext>();

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

        services.AddScoped<IUserRepository, UserRepository>();
    }

    private static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        
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