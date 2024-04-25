namespace Motorent.Api.IntegrationTests.Common;

public abstract class WebApiFactoryFixture(WebApiFactory api) : IClassFixture<WebApiFactory>, IAsyncLifetime
{
    private HttpClient? client;

    protected HttpClient Client => client ??= api.CreateClient();

    protected DbContext DataContext => api.DataContext;

    protected Task ResetDatabaseAsync() => api.ResetDatabaseAsync();

    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync() => api.ResetDatabaseAsync();
}