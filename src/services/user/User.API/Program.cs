using User.API.Profiles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using User.API.Auth;
using Microsoft.OpenApi.Models;
using User.Service;
using User.Infra;
using User.Infra.Messaging.Extensions;
using BusConnections.Events;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var secretKey = builder.Configuration.GetValue<string>("Jwt:Key");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(ctx =>  {
    ctx.SwaggerDoc("v1", new OpenApiInfo {
        Title = "Challenge API",
        Version = "v1",
        Description = "The is API is builded for win the challenge",
        Contact = new OpenApiContact {
            Name = "Lismar Oliveira",
            Email ="englismarOliveira@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/lismar-oliveira-9a93ba94/")
        }
    });

    ctx.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
        In = ParameterLocation.Header,
        Description = "The bearer token is mandatory.",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    
    ctx.AddSecurityRequirement(new OpenApiSecurityRequirement()
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              }
            },
            new List<string>()
          }
        });
});

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
builder.Services.AddControllers();
builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();
builder.Services.AddAutoMapper(typeof(PartnerProfile));
builder.Services.AddAuthentication(context =>
{
    context.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    context.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(context =>
{
    context.RequireHttpsMetadata = false;
    context.SaveToken = true;
    context.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey!)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
     {
     	c.SwaggerEndpoint("/swagger/v1/swagger.json", "User API V1");
     	c.RoutePrefix = "user/docs";
     });
}

app.UseHttpsRedirection();
var serviceProvider = builder.Services.BuildServiceProvider();
app.UseRabbitListener(serviceProvider);
app.MapControllers();

app.Run();

