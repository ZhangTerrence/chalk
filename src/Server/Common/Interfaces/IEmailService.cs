namespace Server.Common.Interfaces;

public interface IEmailService
{
  public void SendEmail(string to, string subject, string body);
}