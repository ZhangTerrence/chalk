namespace Server.Common.Interfaces;

/// <summary>
/// Interface for email services.
/// </summary>
public interface IEmailService
{
  /// <summary>
  /// Sends an email.
  /// </summary>
  /// <param name="to">The receiver's email.</param>
  /// <param name="subject">The email's subject.</param>
  /// <param name="body">The email's body.</param>
  public void SendEmail(string to, string subject, string body);
}
