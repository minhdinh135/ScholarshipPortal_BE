using System.Reflection;

namespace Application.Helper;

public class EnumHelper
{
    public static List<string> ConvertEnumToList<T>()
    {
        return Enum.GetNames(typeof(T)).ToList();
    }

    public static List<string?> GetConstantValues<T>()
    {
        return typeof(T)
            .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
            .Where(field => field.IsLiteral && !field.IsInitOnly)
            .Where(field => field.FieldType == typeof(string))
            .Select(field => field.GetValue(null) as string)
            .ToList();
    }
}