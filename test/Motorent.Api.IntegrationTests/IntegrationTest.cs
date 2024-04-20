using Microsoft.Extensions.DependencyInjection;
using Motorent.Infrastructure.Common.Persistence;

namespace Motorent.Api.IntegrationTests;

public abstract class IntegrationTest : IClassFixture<WebAppFactory>, IDisposable, IAsyncLifetime
{
    private readonly WebAppFactory app;
    private readonly IServiceScope serviceScope;

    protected IntegrationTest(WebAppFactory app)
    {
        this.app = app;
        
        serviceScope = app.Services.CreateScope();
        DataContext = serviceScope.ServiceProvider.GetRequiredService<DataContext>();
    }

    protected HttpClient Client => app.Client;
    
    protected DbContext DataContext { get; }
    
    protected Task ResetDatabaseAsync() => app.ResetDatabaseAsync();

    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync() => app.ResetDatabaseAsync();

    public void Dispose()
    {
        serviceScope.Dispose();
        GC.SuppressFinalize(this);
    }
}