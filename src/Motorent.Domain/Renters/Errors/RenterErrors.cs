namespace Motorent.Domain.Renters.Errors;

internal static class RenterErrors
{
    public static readonly Error DuplicateCnpj = Error.Conflict(
        "There is already a renter with the same CNPJ.", code: "renter.duplicate_cnpj");
    
    public static readonly Error DuplicateCnh = Error.Conflict(
        "There is already a renter with the same CNH.", code: "renter.duplicate_cnh");
}