using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using Motorent.Application.Common.Abstractions.Security;
using Motorent.Domain.Users;
using Motorent.Domain.Users.Enums;
using Motorent.Domain.Users.ValueObjects;
using Motorent.Infrastructure.Renters.Persistence.Configuration;
using Motorent.Infrastructure.Users;
using Motorent.Infrastructure.Users.Persistence.Configuration;
using Motorent.TestUtils.Constants;

namespace Motorent.Api.IntegrationTests.Common;

public abstract class WebApiFactoryFixture(WebApiFactory api) : IClassFixture<WebApiFactory>, IAsyncLifetime
{
    private readonly IServiceScope serviceScope = api.Services.CreateScope();
    private HttpClient? client;

    protected HttpClient Client => client ??= api.CreateClient();

    protected DbContext DataContext => api.DataContext;

    protected Task ResetDatabaseAsync() => api.ResetDatabaseAsync();

    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync()
    {
        serviceScope.Dispose();
        return api.ResetDatabaseAsync();
    }

    protected async Task AuthenticateAsync(UserId userId)
    {
        var user = await DataContext.Set<User>()
            .SingleOrDefaultAsync(u => u.Id == userId);

        if (user is null)
        {
            throw new InvalidOperationException(
                $"User with id {userId} not found. Make sure to create the user before authenticating.");
        }

        var securityTokenService = serviceScope.ServiceProvider.GetRequiredService<ISecurityTokenService>();
        var securityToken = await securityTokenService.GenerateTokenAsync(user);

        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Bearer", securityToken.AccessToken);
    }

    protected async Task<UserId> CreateUserAsync(UserData data)
    {
        var encryptionService = new EncryptionService();
        var passwordHash = encryptionService.Encrypt(data.Password);
        var sql = $"""
                   INSERT INTO {UserConstants.TableName} (id, role, name, birthdate, email, password_hash)
                   VALUES ('{data.UserId}',
                           '{data.Role.Name}',
                           '{data.Name.Value}',
                           '{data.Birthdate.Value.ToString()}',
                           '{data.Email}',
                           '{passwordHash}')
                   """;

        await DataContext.Database.ExecuteSqlRawAsync(sql);

        if (data.Role == Role.Renter)
        {
            await CreateRenterAsync(data);
        }

        return data.UserId;
    }

    private async Task CreateRenterAsync(UserData data)
    {
        var sql = $"""
                   INSERT INTO {RenterConstants.TableName} (
                                                              id,
                                                              user_id,
                                                              cnpj,
                                                              cnh_number,
                                                              cnh_category,
                                                              cnh_exp_date,
                                                              name,
                                                              birthdate)
                   VALUES ('{Constants.Renter.Id}',
                           '{data.UserId}',
                           '{Constants.Renter.Cnpj}',
                           '{Constants.Renter.Cnh.Number.Value}',
                           '{Constants.Renter.Cnh.Category.Name}',
                           '{Constants.Renter.Cnh.ExpirationDate.ToString()}',
                           '{data.Name.Value}',
                           '{data.Birthdate.Value.ToString()}')
                   """;

        await DataContext.Database.ExecuteSqlRawAsync(sql);
    }
}