
using MT.Backend.Challenge.CrossCutting.DependencyInjection;
using MT.Backend.Challenge.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MT.Backend.Challenge.Domain.Entities.configs;

namespace MT.Backend.Challenge.Api
{
    public static class Program
    {
      

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            builder.Services.Configure<QueueServerConfig>(
               configuration.GetSection(nameof(QueueServerConfig)));
            builder.Services.Configure<ImageServiceConfig>(
               configuration.GetSection(nameof(ImageServiceConfig)));

            // Add services to the container.
            builder.Services.AddAutoMapper();
            builder.Services.AddMediator();
            builder.Services.AddRepository();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowMyOrigin", builder =>
                {
                    builder.WithOrigins(
                        "http://localhost:3000"
                    )
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithExposedHeaders("X-Total-Count");
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowMyOrigin");

            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();
        }
    }
}
