namespace chalk.Server.Services.Interfaces;

public interface IEmailService
{
    public void SendEmail(string to, string subject, string body);
}