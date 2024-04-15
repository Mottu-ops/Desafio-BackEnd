using System.Linq.Expressions;

namespace Motorent.Application.Common.Mappings;

internal static class StringTransformFunctions
{
    public static readonly Expression<Func<string, string>> Trim = str =>
        string.IsNullOrEmpty(str) ? str : str.Trim();
}