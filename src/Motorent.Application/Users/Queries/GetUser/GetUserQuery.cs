using Motorent.Application.Common.Abstractions.Requests;
using Motorent.Contracts.Users.Responses;

namespace Motorent.Application.Users.Queries.GetUser;

public sealed record GetUserQuery : IQuery<UserResponse>;