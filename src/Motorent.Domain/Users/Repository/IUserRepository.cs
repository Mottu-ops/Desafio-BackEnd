using Motorent.Domain.Common.Repository;
using Motorent.Domain.Users.ValueObjects;

namespace Motorent.Domain.Users.Repository;

public interface IUserRepository : IRepository<User, UserId>;