using chalk.Server.Utilities;

namespace chalk.Server.Common.Errors;

public partial class Errors
{
    public static class User
    {
        public static string UnableToAssignToRole(string role) =>
            $"Unable to assign user to {role.LowercaseFirstLetters()} role.";
    }
}