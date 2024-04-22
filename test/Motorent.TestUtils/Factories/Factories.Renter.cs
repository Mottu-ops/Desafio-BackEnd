using Motorent.Domain.Common.ValueObjects;
using Motorent.Domain.Renters.ValueObjects;
using Motorent.Domain.Users.ValueObjects;

namespace Motorent.TestUtils.Factories;

public static partial class Factories
{
    public static class Renter
    {
        public static Domain.Renters.Renter CreateRenter(
            RenterId? id = null,
            UserId? userId = null,
            Cnpj? cnpj = null,
            Cnh? cnh = null,
            Name? name = null,
            Birthdate? birthdate = null)
        {
            return new Domain.Renters.Renter(
                id ?? Constants.Constants.Renter.Id,
                userId ?? Constants.Constants.User.Id,
                cnpj ?? Constants.Constants.Renter.Cnpj,
                cnh ?? Constants.Constants.Renter.Cnh,
                name ?? Constants.Constants.User.Name,
                birthdate ?? Constants.Constants.User.Birthdate);
        }
    }
}