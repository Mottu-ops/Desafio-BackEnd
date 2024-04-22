namespace Motorent.Application.Common.Abstractions.Security.Authorization;

internal interface IAuthorizer<in TSubject> where TSubject : class
{
    IEnumerable<IAuthorizationRequirement> GetRequirements(TSubject subject);
}