using System.Net;
using System.Net.Mail;
using Server.Common.Interfaces;

namespace Server.Services;

internal class EmailService : IEmailService
{
  private const string SmtpHost = "smtp.gmail.com";
  private const int SmtpPort = 587;
  private const string SmtpName = "Chalk";

  private readonly string _senderEmail;
  private readonly string _senderPassword;

  public EmailService(IConfiguration configuration)
  {
    this._senderEmail = configuration["SMTP:Email"]!;
    this._senderPassword = configuration["SMTP:Password"]!;
  }

  public void SendEmail(string to, string subject, string body)
  {
    var sender = new MailAddress(this._senderEmail, SmtpName);
    var receiver = new MailAddress(to);
    var smtpClient = new SmtpClient
    {
      Host = SmtpHost,
      Port = SmtpPort,
      EnableSsl = true,
      DeliveryMethod = SmtpDeliveryMethod.Network,
      UseDefaultCredentials = false,
      Credentials = new NetworkCredential(this._senderEmail, this._senderPassword)
    };
    using var message = new MailMessage(sender, receiver);
    message.Subject = subject;
    message.Body = body;
    smtpClient.Send(message);
  }
}
