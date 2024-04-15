using Ardalis.SmartEnum;

namespace Motorent.Domain.Common.Enums;

public abstract class Enumeration<T>(string name, int value) : SmartEnum<T>(name, value) where T : SmartEnum<T, int>
{
    public static bool IsDefined(string name, bool ignoreCase = false) => TryFromName(name, ignoreCase, out _);
}