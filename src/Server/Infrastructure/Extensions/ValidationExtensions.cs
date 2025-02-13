using System.Reflection;
using FluentValidation;

namespace Server.Infrastructure.Extensions;

internal static class ValidationExtensions
{
  public static void AddValidation(this WebApplicationBuilder builder)
  {
    builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), ServiceLifetime.Singleton);
  }
}
