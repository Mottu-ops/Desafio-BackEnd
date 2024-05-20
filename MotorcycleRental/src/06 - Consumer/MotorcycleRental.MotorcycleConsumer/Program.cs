using MotorcycleRental.Infraestructure;
using MotorcycleRental.MotorcycleConsumer.Config;
using MotorcycleRental.MotorcycleConsumer.ConsumerService;
using RabbitMqMessage;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddInfraestructureService(builder.Configuration);
builder.Services.AddHealthChecksConfiguration(builder.Configuration);
builder.Services.AddHostedService<Year2024Consumer>();

builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection("RabbitMqSettings"));
builder.Services.Configure<MailQueueSettings>(builder.Configuration.GetSection("MailQueueSettings"));
builder.Services.Configure<EmailList>(builder.Configuration.GetSection("EmailList"));

builder.Services.AddRabbitMqMessage();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHealthChecksConfiguration();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
