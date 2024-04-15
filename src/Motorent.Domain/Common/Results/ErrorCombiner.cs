using System.Collections.Immutable;

namespace Motorent.Domain.Common.Results;

public static class ErrorCombiner
{
    public static ImmutableArray<Error> Combine(params IResult[] results) =>
    [
        ..results
            .Where(result => result.IsFailure)
            .SelectMany(result => result.Errors)
    ];
}