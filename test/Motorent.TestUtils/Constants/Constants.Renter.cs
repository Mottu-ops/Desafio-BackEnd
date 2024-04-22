using Motorent.Domain.Renters.Enums;
using Motorent.Domain.Renters.ValueObjects;

namespace Motorent.TestUtils.Constants;

public static partial class Constants
{
    public static class Renter
    {
        public static readonly RenterId Id = RenterId.New();

        public static readonly Cnpj Cnpj = Cnpj.Create("").Value;

        public static readonly Cnh Cnh = Cnh.Create(
                number: CnhNumber.Create("").Value,
                expirationDate: new DateOnly(DateTime.Today.Year + 5, 1, 1),
                category: CnhCategory.A)
            .Value;
    }
}