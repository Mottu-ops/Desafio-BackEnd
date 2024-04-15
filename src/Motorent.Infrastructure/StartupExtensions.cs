using Microsoft.AspNetCore.Builder;

namespace Motorent.Infrastructure;

public static class StartupExtensions
{
    public static void UseInfrastructure(this WebApplication app)
    {
        app.UseAuthentication();
        
        app.UseAuthorization();
    }
}