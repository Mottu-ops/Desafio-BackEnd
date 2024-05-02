using Job.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Job.IntegrationTest.Fixtures;

public class DbFixture : IDisposable, IAsyncDisposable
{
    private readonly JobContext _context;
    private readonly string _databaseName = $"Context_{Guid.NewGuid().ToString()}";
    public readonly string ConnectionString;
    private bool _disposed;

    public DbFixture()
    {
        ConnectionString = $"Server=localhost;Port=5432;User Id=postgres;Password=postgres;Database={_databaseName};";

        var builder = new DbContextOptionsBuilder<JobContext>();
        builder.UseNpgsql(ConnectionString);

        _context = new JobContext(builder.Options);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Database.EnsureDeleted();
                _context.Dispose();
            }
        }

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
    }
}