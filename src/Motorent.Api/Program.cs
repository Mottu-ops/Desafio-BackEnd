using Motorent.Application;
using Motorent.Infrastructure;
using Motorent.Presentation;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddApplication()
    .AddInfrastructure()
    .AddPresentation();

var app = builder.Build();

app.UsePresentation();

app.UseInfrastructure();

app.Run();