using MotorcycleRental.AdminManagementService.Api.Config.ConfigApi;
using MotorcycleRental.Api.Core.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfraestructure(builder.Configuration);

builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection("RabbitMQ"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddJwtConfiguration(builder.Configuration);



var app = builder.Build();

app.UseSwaggerConfiguration();
app.UseMiddleware(typeof(ErrorMiddleware));
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseAuthConfiguration();

app.MapControllers();

app.Run();
