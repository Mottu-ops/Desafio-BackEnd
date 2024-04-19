using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Motorent.Infrastructure.Common.Persistence;

namespace Motorent.Api.IntegrationTests;

public abstract class IntegrationTest : IClassFixture<WebAppFactory>, IDisposable
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    private readonly IServiceScope serviceScope;
    
    protected IntegrationTest(WebAppFactory app)
    {
        App = app;
        serviceScope = app.Services.CreateScope();
        DataContext = serviceScope.ServiceProvider.GetRequiredService<DataContext>();
    }
    
    protected WebAppFactory App { get; }
    
    protected DbContext DataContext { get; }
    
    protected static string Serialize<T>(T value) => JsonSerializer.Serialize(value, SerializerOptions);
    
    public void Dispose()
    {
        serviceScope.Dispose();
        GC.SuppressFinalize(this);
    }
}