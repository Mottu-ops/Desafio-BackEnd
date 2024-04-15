using Microsoft.AspNetCore.Builder;

namespace Motorent.Presentation;

public static class StartupExtensions
{
    public static void UsePresentation(this WebApplication app)
    {
        app.UseHttpsRedirection();
        
        app.MapControllers();
    }
}