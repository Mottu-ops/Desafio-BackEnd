using System.Data.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Motorent.Infrastructure.Common.Persistence;
using Npgsql;
using Respawn;
using Testcontainers.PostgreSql;

namespace Motorent.IntegrationTests;

public sealed class WebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer postgreSqlContainer = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithDatabase("motorent")
        .WithUsername("root")
        .WithPassword("password")
        .Build();

    private DbConnection postgreSqlConnection = null!;
    private Respawner respawner = null!;

    public HttpClient Client { get; private set; } = null!;
    
    public Task ResetDatabaseAsync() => respawner.ResetAsync(postgreSqlConnection);

    public async Task InitializeAsync()
    {
        await postgreSqlContainer.StartAsync();

        postgreSqlConnection = new NpgsqlConnection(postgreSqlContainer.GetConnectionString());
        
        Client = CreateClient();
        
        await postgreSqlConnection.OpenAsync();

        respawner = await Respawner.CreateAsync(postgreSqlConnection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
            SchemasToInclude = ["public"],
            TablesToIgnore = ["__EFMigrationsHistory"]
        });
    }

    public new Task DisposeAsync() => postgreSqlContainer.StopAsync();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<DbContextOptions<DataContext>>();

            services.AddDbContext<DataContext>(options => options
                .UseNpgsql(postgreSqlContainer.GetConnectionString()));

            services.AddDbContext<DataContext>(options => options
                .UseNpgsql(postgreSqlContainer.GetConnectionString(), pgsqlOptions =>
                    pgsqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

            using var serviceScope = services.BuildServiceProvider()
                .CreateScope();

            MigrateDatabase(serviceScope);
        });
    }

    private static void MigrateDatabase(IServiceScope serviceScope)
    {
        var context = serviceScope.ServiceProvider.GetRequiredService<DataContext>();
        context.Database.EnsureDeleted();
        context.Database.Migrate();
    }
}