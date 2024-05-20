using MotorcycleRental.Infraestructure;
using MotorcycleRental.MailServiceConsumer.Config;
using MotorcycleRental.MailServiceConsumer.ConsumerService;
using MotorcycleRental.MailServiceConsumer.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfraestructureService(builder.Configuration);
builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection("RabbitMqSettings"));
builder.Services.Configure<SendMailConfig>(builder.Configuration.GetSection("SendMailConfig"));
builder.Services.Configure<MailQueueSettings>(builder.Configuration.GetSection("MailQueueSettings"));

builder.Services.AddSingleton<INotifyService, NotifyService>();
builder.Services.AddHostedService<SendMailConsumer>();



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
