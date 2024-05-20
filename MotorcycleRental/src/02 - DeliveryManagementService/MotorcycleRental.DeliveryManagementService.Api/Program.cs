using MotorcycleRental.Api.Core.Identity;
using MotorcycleRental.DeliveryManagementService.Api.Config;
using MotorcycleRental.DeliveryManagementService.Api.Config.ConfigApi;
using MotorcycleRental.DeliveryManagementService.Api.Config.Middlewares;
using MotorcycleRental.DeliveryManagementService.Api.ServiceConsumer;
using MotorcycleRental.DeliveryManagementService.Service;
using MotorcycleRental.Infraestructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfraestructure(builder.Configuration);

builder.Services.Configure<RegisterQueueSettings>(builder.Configuration.GetSection("RegisterQueueSettings"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddHostedService<RegisterUserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwaggerConfiguration();
app.UseMiddleware(typeof(ErrorMiddleware));
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseAuthConfiguration();

app.MapControllers();

app.Run();
