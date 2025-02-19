using System.Diagnostics.CodeAnalysis;

namespace Server.Common.Exceptions;

internal class ServiceException : Exception
{
  public ServiceException(int httpStatusCode, IDictionary<string, string[]> errors)
  {
    this.HttpStatusCode = httpStatusCode;
    this.Errors = errors;
  }

  public int HttpStatusCode { get; }
  public IDictionary<string, string[]> Errors { get; }

  public static void BadRequest(string detail, [NotNull] object? _ = null)
  {
    throw new ServiceException(StatusCodes.Status400BadRequest,
      new Dictionary<string, string[]> { { "Bad Request.", [detail] } });
  }

  public static void BadRequest(IDictionary<string, string[]> errors, [NotNull] object? _ = null)
  {
    throw new ServiceException(StatusCodes.Status400BadRequest, errors);
  }

  public static void Unauthorized(string detail, [NotNull] object? _ = null)
  {
    throw new ServiceException(StatusCodes.Status401Unauthorized,
      new Dictionary<string, string[]> { { "Unauthorized.", [detail] } });
  }

  public static void Unauthorized(IDictionary<string, string[]> errors, [NotNull] object? _ = null)
  {
    throw new ServiceException(StatusCodes.Status401Unauthorized, errors);
  }

  public static void Forbidden(string detail, [NotNull] object? _ = null)
  {
    throw new ServiceException(StatusCodes.Status403Forbidden,
      new Dictionary<string, string[]> { { "Forbidden.", [detail] } });
  }

  public static void Forbidden(IDictionary<string, string[]> errors, [NotNull] object? _ = null)
  {
    throw new ServiceException(StatusCodes.Status403Forbidden, errors);
  }

  public static void NotFound(string detail, [NotNull] object? _ = null)
  {
    throw new ServiceException(StatusCodes.Status404NotFound,
      new Dictionary<string, string[]> { { "Not Found", [detail] } });
  }

  public static void NotFound(IDictionary<string, string[]> errors, [NotNull] object? _ = null)
  {
    throw new ServiceException(StatusCodes.Status404NotFound, errors);
  }

  public static void Conflict(string detail, [NotNull] object? _ = null)
  {
    throw new ServiceException(StatusCodes.Status409Conflict,
      new Dictionary<string, string[]> { { "Conflict.", [detail] } });
  }

  public static void Conflict(IDictionary<string, string[]> errors, [NotNull] object? _ = null)
  {
    throw new ServiceException(StatusCodes.Status409Conflict, errors);
  }

  public static void InternalServerError(string detail, [NotNull] object? _ = null)
  {
    throw new ServiceException(StatusCodes.Status500InternalServerError,
      new Dictionary<string, string[]> { { "Internal Server Error.", [detail] } });
  }

  public static void InternalServerError(IDictionary<string, string[]> errors, [NotNull] object? _ = null)
  {
    throw new ServiceException(StatusCodes.Status500InternalServerError, errors);
  }
}
