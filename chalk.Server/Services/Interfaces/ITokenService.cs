namespace chalk.Server.Services.Interfaces;

public interface ITokenService
{
    public string CreateAccessToken(string email, IEnumerable<string> roles);

    public string CreateRefreshToken();
}