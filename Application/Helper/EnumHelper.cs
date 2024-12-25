namespace Application.Helper;

public class EnumHelper
{
    public static List<string> ConvertEnumToList<T>()
    {
        return Enum.GetNames(typeof(T)).ToList();
    }
}