using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using User.API.Auth;
using User.API.Interfaces.Auth;
using User.API.Profiles;
using User.Infra;
using User.S3.Lib;
using User.Services;
using User.Services.Service;


var builder = WebApplication.CreateBuilder(args);
var secretKey = builder.Configuration.GetValue<string>("JwtSettings:Secret");

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "RandanDelivery User API",
        Version = "v1",
        Description = "This API is builded for challenge in the other Company",
        Contact = new OpenApiContact
        {
            Name = "Vinícius A. Marcarini",
            Email = "viniciusantoniomarcarini@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/marcarinivinicius/")
        }
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "The Bearer token is required.",
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
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


builder.Services.AddInfraModules();
builder.Services.AddS3LibModules();
builder.Services.AddServicesModules();

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();
builder.Services.AddScoped<IJwtService, JwtService>();


builder.Services.AddAuthentication(context =>
{
    context.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    context.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(context =>
{
    context.RequireHttpsMetadata = false;
    context.SaveToken = true;
    context.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey!)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
