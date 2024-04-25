using BusConnections.Events;
using RabbitMQ.Client;
using Rent.API.Profilers;
using Rent.Infra;
using User.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(RentProfiler));



builder.Services.AddSingleton<IBusConnection>(sp =>
{
    var factory = new ConnectionFactory()
    {
        HostName = builder.Configuration["BusConnection:HostName"]

    };
    factory.AutomaticRecoveryEnabled = true;
    factory.NetworkRecoveryInterval = TimeSpan.FromSeconds(5);
    factory.TopologyRecoveryEnabled = true;

    if (!string.IsNullOrWhiteSpace(builder.Configuration["BusConnection:UserName"]))
        factory.UserName = builder.Configuration["BusConnection:UserName"];

    if (!string.IsNullOrWhiteSpace(builder.Configuration["BusConnection:Password"]))
        factory.Password = builder.Configuration["BusConnection:Password"];
    var retryCount = 10;

    if (!string.IsNullOrWhiteSpace(builder.Configuration["BusConnection:RetryCount"]))
        retryCount = int.Parse(builder.Configuration["BusConnection:RetryCount"]);

    return new BusConnection(factory, retryCount);
});
builder.Services.AddInfraModules();
builder.Services.AddServiceModules();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
     {
     	c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rent API V1");
     	c.RoutePrefix = "rent/docs";
     });
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

