using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using FluentValidation.Internal;

namespace Motorent.Application.Common.Validations;

internal static class PropertyNameResolvers
{
    public static string SnakeCaseResolver(Type type, MemberInfo member, LambdaExpression expression)
    {
        var chain = PropertyChain.FromExpression(expression);
        var name = chain.Count > 0 ? chain.ToString() : member.Name;
        var parts = new List<string>();
        var word = new StringBuilder();

        foreach (var c in name)
        {
            if (char.IsUpper(c) && word.Length > 0)
            {
                parts.Add(word.ToString());
                word.Clear();
            }

            word.Append(char.ToLower(c));
        }

        if (word.Length > 0)
        {
            parts.Add(word.ToString());
        }

        return string.Join("_", parts.ToArray());
    }
}