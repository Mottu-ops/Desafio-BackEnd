using Microsoft.OpenApi.Models;
using MotorcycleRental.Authentication.Api.Extensions;

namespace MotorcycleRental.Authentication.Api.Configuration
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Authentication Api",
                    Description = "This Api aims to Register and Authenticate users.",
                    Contact = new OpenApiContact() { Name = "Cleber Trindade", Email = "cleber.trindade.net@gmail.com" },
                    License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/license/mit/") }
                });

            });
            return services;
        }

        public static WebApplication UseSwaggerConfiguration(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                });
                app.ApplyMigrations();
            }

            return app;
        }
    }
}
