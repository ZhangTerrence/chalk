using System.Globalization;

namespace chalk.Server.Utilities;

public static class StringUtilities
{
    public static string UppercaseFirstLetters(this string s)
    {
        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
    }

    public static string LowercaseFirstLetters(this string s)
    {
        return s.ToLower();
    }
}