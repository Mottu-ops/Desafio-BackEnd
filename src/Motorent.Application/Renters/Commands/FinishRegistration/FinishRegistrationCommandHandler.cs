using Motorent.Application.Common.Abstractions.Identity;
using Motorent.Application.Common.Abstractions.Requests;
using Motorent.Contracts.Renters.Responses;
using Motorent.Domain.Renters.Enums;
using Motorent.Domain.Renters.Repository;
using Motorent.Domain.Renters.Services;
using Motorent.Domain.Renters.ValueObjects;
using Motorent.Domain.Users.Repository;

namespace Motorent.Application.Renters.Commands.FinishRegistration;

internal sealed class FinishRegistrationCommandHandler(
    IUserContext userContext,
    IUserRepository userRepository,
    IRenterRepository renterRepository,
    IRenterFactory renterFactory)
    : ICommandHandler<FinishRegistrationCommand, RenterResponse>
{
    public async Task<Result<RenterResponse>> Handle(FinishRegistrationCommand command,
        CancellationToken cancellationToken)
    {
        var user = await userRepository.FindAsync(userContext.UserId, cancellationToken);
        if (user is null)
        {
            throw new ApplicationException($"User {userContext.UserId} not found");
        }

        var cnpj = Cnpj.Create(command.Cnpj);
        var cnhNumber = CnhNumber.Create(command.CnhNumber);
        var cnhCategory = CnhCategory.FromName(command.CnhCategory);

        var errors = ErrorCombiner.Combine(cnpj, cnhNumber);
        if (errors.Any())
        {
            return errors;
        }

        var cnh = Cnh.Create(cnhNumber.Value, command.CnhExpirationDate, cnhCategory);
        if (cnh.IsFailure)
        {
            return cnh.Errors;
        }

        var result = renterFactory.CreateAsync(
            user,
            RenterId.New(),
            cnpj.Value,
            cnh.Value,
            cancellationToken);

        return await result
            .ThenAsync(renter => renterRepository.AddAsync(renter, cancellationToken))
            .Then(renter => renter.Adapt<RenterResponse>());
    }
}