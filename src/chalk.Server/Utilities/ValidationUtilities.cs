using FluentValidation.Results;

namespace chalk.Server.Utilities;

public static class ValidationUtilities
{
    public static IEnumerable<string> GetErrorMessages(this ValidationResult validationResult)
    {
        return validationResult.Errors.Select(error => error.ErrorMessage);
    }
}