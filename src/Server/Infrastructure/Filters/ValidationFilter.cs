using System.Reflection;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Server.Common.Responses;
using Server.Common.Utilities;

namespace Server.Infrastructure.Filters;

[AttributeUsage(AttributeTargets.Parameter)]
public class ValidateAttribute : Attribute;

public abstract class ValidationFilter
{
  public static EndpointFilterDelegate Create(EndpointFilterFactoryContext context, EndpointFilterDelegate next)
  {
    var validationDescriptors = GetValidators(context.MethodInfo, context.ApplicationServices).ToList();
    return validationDescriptors.Count != 0
      ? invocationContext => Validate(validationDescriptors, invocationContext, next)
      : next;
  }

  private static async ValueTask<object?> Validate(
    IEnumerable<ValidationDescriptor> validationDescriptors,
    EndpointFilterInvocationContext invocationContext,
    EndpointFilterDelegate next
  )
  {
    foreach (var descriptor in validationDescriptors)
    {
      var argument = invocationContext.Arguments[descriptor.Index];
      if (argument is null) continue;
      var validationResult = await descriptor.Validator.ValidateAsync(new ValidationContext<object>(argument));
      if (!validationResult.IsValid)
        return new BadRequestObjectResult(new Response<object>(validationResult.GetErrorMessages()));
    }
    return await next.Invoke(invocationContext);
  }

  private static IEnumerable<ValidationDescriptor> GetValidators(
    MethodInfo methodInfo,
    IServiceProvider serviceProvider
  )
  {
    foreach (var e in methodInfo.GetParameters().Select((parameter, index) => new { parameter, index }))
      if (e.parameter.GetCustomAttribute<ValidateAttribute>() is not null)
      {
        var validatorType = typeof(IValidator<>).MakeGenericType(e.parameter.ParameterType);
        if (serviceProvider.GetService(validatorType) is IValidator validator)
          yield return new ValidationDescriptor(e.index, validator);
      }
  }

  private sealed record ValidationDescriptor(int Index, IValidator Validator);
}