using Microsoft.AspNetCore.Builder;
using Serilog;

namespace Motorent.Infrastructure;

public static class StartupExtensions
{
    public static void UseInfrastructure(this WebApplication app)
    {
        app.UseSerilogRequestLogging();
        
        app.UseAuthentication();
        
        app.UseAuthorization();
    }
}