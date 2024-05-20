using Microsoft.EntityFrameworkCore;
using MotorcycleRental.Infraestructure.Context.Postgress;

namespace MotorcycleRental.DeliveryManagementService.Api.Config.Extensions
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using ApplicationDbContext dbContext =
                scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            dbContext.Database.Migrate();
        }
    }
}
