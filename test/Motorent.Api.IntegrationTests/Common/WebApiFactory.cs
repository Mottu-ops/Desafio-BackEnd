using System.Collections.Specialized;
using System.Data.Common;
using DotNet.Testcontainers.Builders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Motorent.Infrastructure.Common.Persistence;
using Quartz;
using Quartz.Impl;
using Respawn;
using Testcontainers.PostgreSql;

namespace Motorent.Api.IntegrationTests.Common;

public sealed class WebApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer databaseContainer = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithDatabase("motorent")
        .WithUsername("root")
        .WithPassword("password")
        .WithWaitStrategy(Wait.ForUnixContainer()
            .UntilCommandIsCompleted("pg_isready"))
        .Build();

    private Respawner respawner = null!;
    private DbConnection connection = null!;

    public DbContext DataContext { get; private set; } = null!;

    public Task ResetDatabaseAsync() => respawner.ResetAsync(connection);

    public async Task InitializeAsync()
    {
        await databaseContainer.StartAsync();

        DataContext = Services.CreateScope()
            .ServiceProvider.GetRequiredService<DataContext>();

        EnsureDatabaseCreated();

        await InitializeRespawner();
    }

    public new async Task DisposeAsync()
    {
        await connection.CloseAsync();
        await databaseContainer.StopAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            ConfigureQuartzService(services);
            ConfigureDataContextService(services);
        });
    }

    private static void ConfigureQuartzService(IServiceCollection services)
    {
        services.RemoveAll<ISchedulerFactory>();
        services.AddSingleton<ISchedulerFactory>(_ => new StdSchedulerFactory(
            new NameValueCollection
            {
                ["quartz.scheduler.instanceName"] = Guid.NewGuid().ToString()
            }));
    }

    private void ConfigureDataContextService(IServiceCollection services)
    {
        services.RemoveAll<DbContextOptions<DataContext>>();
        services.AddDbContext<DataContext>(options => options
            .UseNpgsql(databaseContainer.GetConnectionString(), pgsqlOptions =>
                pgsqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));
    }

    private void EnsureDatabaseCreated()
    {
        DataContext.Database.EnsureDeleted();
        DataContext.Database.Migrate();
    }

    private async Task InitializeRespawner()
    {
        connection = DataContext.Database.GetDbConnection();

        await connection.OpenAsync();

        respawner = await Respawner.CreateAsync(connection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
            SchemasToInclude = ["public"],
            TablesToIgnore = ["__EFMigrationsHistory"]
        });
    }
}