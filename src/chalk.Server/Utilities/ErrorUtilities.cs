using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;

namespace chalk.Server.Utilities;

public static class ErrorUtilities
{
    public static IEnumerable<string> GetErrorMessages(this ValidationResult validationResult)
    {
        return validationResult.Errors.Select(error => error.ErrorMessage);
    }

    public static IEnumerable<string> GetErrorMessages(this IdentityResult identityResult)
    {
        return identityResult.Errors.Select(error => error.Description);
    }
}