using Motorent.Domain.Common.Enums;

namespace Motorent.Domain.Renters.Enums;

public sealed class CnhCategory : Enumeration<CnhCategory> 
{
    public static readonly CnhCategory A = new("A", 1);
    public static readonly CnhCategory B = new("B", 2);
    public static readonly CnhCategory AB = new("AB", 2);
    
    private CnhCategory(string name, int value) : base(name, value)
    {
    }
}