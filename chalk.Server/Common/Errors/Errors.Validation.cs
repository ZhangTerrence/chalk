namespace chalk.Server.Common.Errors;

public static partial class Errors
{
    public static class Validation
    {
        public static string IsRequired(string property) => $"{property} property is required.";

        public static string IsInvalid(string property) => $"{property} property is invalid.";

        public static string IsBetween(string property, int min, int max) =>
            $"{property} property must have between {min} and {max} characters.";

        public static string IsInvalidPassword =>
            "Password property must have at least 8 characters with least one number, one lowercase letter, one upper case letter, one special character.";
    }
}