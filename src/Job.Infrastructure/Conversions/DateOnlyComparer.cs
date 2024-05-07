using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Job.Infrastructure.Conversions;

[ExcludeFromCodeCoverage]
public sealed class DateOnlyComparer() : ValueComparer<DateOnly>(
    equalsExpression: (dateOnlyOne, dateOnlyTwo) => dateOnlyOne.DayNumber == dateOnlyTwo.DayNumber,
    hashCodeExpression: dateOnly => dateOnly.GetHashCode());