using chalk.Server.Utilities;

namespace chalk.Server.Common.Errors;

public partial class Errors
{
    public static string AlreadyExists(string resource) => $"{resource.UppercaseFirstLetters()} already exists.";

    public static string NotFound(string resource) => $"{resource.UppercaseFirstLetters()} not found.";

    public static string UnableToCreate(string resource) => $"Unable to create {resource.LowercaseFirstLetters()}.";

    public static string UnableToUpdate(string resource) => $"Unable to update {resource.LowercaseFirstLetters()}.";
}