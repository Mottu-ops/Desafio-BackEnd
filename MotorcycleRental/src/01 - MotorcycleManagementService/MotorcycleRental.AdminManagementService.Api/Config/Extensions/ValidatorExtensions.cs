using FluentValidation.Results;
using MotorcycleRental.AdminManagementService.Api.Config.Models;

namespace MotorcycleRental.AdminManagementService.Api.Config.Extensions
{
    public static class ValidatorExtensions
    {
        public static CustomErrorResponse ToCustomErrorResponse(this IList<ValidationFailure> failures)
        {
            var errors = failures.Select(f => f.ErrorMessage).ToArray();
            return new CustomErrorResponse(400, new ErrorDetail(errors.ToList()));
        }
    }
}
