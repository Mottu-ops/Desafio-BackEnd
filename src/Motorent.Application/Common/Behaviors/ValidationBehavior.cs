using FluentValidation;
using FluentValidation.Results;

namespace Motorent.Application.Common.Behaviors;

internal sealed class ValidationBehavior<TRequest, TResponse>(IValidator<TRequest>? validator = null)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IResult
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (validator is null)
        {
            return await next();
        }

        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        return validationResult.IsValid
            ? await next()
            : (TResponse)(dynamic)ConvertValidationFailuresToResult(validationResult.Errors);
    }

    private static List<Error> ConvertValidationFailuresToResult(List<ValidationFailure> failures) =>
        failures.ConvertAll(f => Error.Validation(message: f.ErrorMessage, code: f.PropertyName));
}