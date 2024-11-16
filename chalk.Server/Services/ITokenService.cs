namespace chalk.Server.Services;

public interface ITokenService
{
    public string CreateAccessToken(string displayName, IEnumerable<string> roles);
    public string CreateRefreshToken();
}