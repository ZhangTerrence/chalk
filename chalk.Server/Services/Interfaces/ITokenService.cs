namespace chalk.Server.Services.Interfaces;

public interface ITokenService
{
    public string CreateAccessToken(long userId, string fullName, IEnumerable<string> roles);

    public string CreateRefreshToken();
}