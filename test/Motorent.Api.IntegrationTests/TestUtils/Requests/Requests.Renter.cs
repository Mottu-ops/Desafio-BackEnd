using Motorent.Contracts.Renters.Requests;

namespace Motorent.Api.IntegrationTests.TestUtils.Requests;

internal static partial class Requests
{
    public static class Renter
    {
        public static readonly FinishRegistrationRequest FinishRegistrationRequest = new()
        {
            Cnpj = "53.965.140/0001-60",
            CnhNumber = "70632908090",
            CnhCategory = "AB",
            CnhExpirationDate = DateOnly.FromDateTime(DateTime.Today.AddYears(2))
        };

        public static HttpRequestMessage FinishRegistrationHttpRequest(FinishRegistrationRequest request) =>
            Post("v1/renters/finish-registration", request);
    }

}