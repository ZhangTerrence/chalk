using System.Diagnostics.CodeAnalysis;

namespace Server.Common.Exceptions;

internal class ServiceException : Exception
{
  public ServiceException(int httpStatusCode, IEnumerable<string> messages)
  {
    this.HttpStatusCode = httpStatusCode;
    this.Messages = messages;
  }

  public int HttpStatusCode { get; }
  public IEnumerable<string> Messages { get; }

  public static void BadRequest(string message, [NotNull] object? e = null)
  {
    throw new ServiceException(StatusCodes.Status400BadRequest, [message]);
  }

  public static void BadRequest(IEnumerable<string> messages, [NotNull] object? e = null)
  {
    throw new ServiceException(StatusCodes.Status400BadRequest, messages);
  }

  public static void Unauthorized(string message, [NotNull] object? e = null)
  {
    throw new ServiceException(StatusCodes.Status401Unauthorized, [message]);
  }

  public static void Unauthorized(IEnumerable<string> messages, [NotNull] object? e = null)
  {
    throw new ServiceException(StatusCodes.Status401Unauthorized, messages);
  }

  public static void Forbidden(string message, [NotNull] object? e = null)
  {
    throw new ServiceException(StatusCodes.Status403Forbidden, [message]);
  }

  public static void Forbidden(IEnumerable<string> messages, [NotNull] object? e = null)
  {
    throw new ServiceException(StatusCodes.Status403Forbidden, messages);
  }

  public static void NotFound(string message, [NotNull] object? e = null)
  {
    throw new ServiceException(StatusCodes.Status404NotFound, [message]);
  }

  public static void NotFound(IEnumerable<string> messages, [NotNull] object? e = null)
  {
    throw new ServiceException(StatusCodes.Status404NotFound, messages);
  }

  public static void InternalServerError(string message, [NotNull] object? e = null)
  {
    throw new ServiceException(StatusCodes.Status500InternalServerError, [message]);
  }

  public static void InternalServerError(IEnumerable<string> messages, [NotNull] object? e = null)
  {
    throw new ServiceException(StatusCodes.Status500InternalServerError, messages);
  }
}
