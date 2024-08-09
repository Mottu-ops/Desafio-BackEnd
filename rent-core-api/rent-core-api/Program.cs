using rent_core_api.RabbitMQ;
using rent_core_api.Repository;
using rent_core_api.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<MotoRepository, MotoRepositoryImpl>();
builder.Services.AddTransient<MotoService, MotoServiceImpl>();
builder.Services.AddTransient<EntregadorRepository, EntregadorRepositoryImpl>();
builder.Services.AddTransient<EntregadorService, EntregadorServiceImpl>();
builder.Services.AddTransient<PlanoRepository, PlanoRepositoryImpl>();
builder.Services.AddTransient<LocacaoRepository, LocacaoRepositoryImpl>();
builder.Services.AddTransient<LocacaoService, LocacaoServiceImpl>();

builder.Services.AddSingleton<RabbitMqPublisher>();
builder.Services.AddSingleton<RabbitMqConsumer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//var consumer = app.Services.GetRequiredService<RabbitMqConsumer>();
//consumer.StartConsuming();


app.Run();
