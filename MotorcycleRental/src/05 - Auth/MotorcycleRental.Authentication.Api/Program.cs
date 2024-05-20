using MotorcycleRental.Api.Core.Identity;
using MotorcycleRental.Authentication.Api.Configuration;
using MotorcycleRental.Infraestructure;
using RabbitMqMessage;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfraestructure(builder.Configuration);
builder.Services.AddIdentityConfiguration(builder.Configuration);
builder.Services.AddSwaggerConfiguration();
builder.Services.AddApiConfiguration();
builder.Services.AddRabbitMqMessage();

builder.Services.Configure<RegisterQueueSettings>(builder.Configuration.GetSection("RegisterQueueSettings"));

var app = builder.Build();

app.UseAuthConfiguration();
app.UseSwaggerConfiguration();
app.UseApiConfiguration();


app.Run();
