using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;

namespace Server.Common.Utilities;

internal static class ErrorUtilities
{
  public static IDictionary<string, string[]> GetErrors(this ValidationResult validationResult)
  {
    return validationResult.Errors
      .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
      .ToDictionary(group => group.Key, group => group.ToArray());
  }

  public static IDictionary<string, string[]> GetErrors(this IdentityResult identityResult)
  {
    return identityResult.Errors
      .GroupBy(e => e.Code, e => e.Description)
      .ToDictionary(group => group.Key, group => group.ToArray());
  }
}
