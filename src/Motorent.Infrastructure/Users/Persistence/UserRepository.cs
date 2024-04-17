using Motorent.Domain.Users;
using Motorent.Domain.Users.Repository;
using Motorent.Domain.Users.ValueObjects;
using Motorent.Infrastructure.Common.Persistence;

namespace Motorent.Infrastructure.Users.Persistence;

internal sealed class UserRepository(DataContext context) : Repository<User, UserId>(context), IUserRepository;