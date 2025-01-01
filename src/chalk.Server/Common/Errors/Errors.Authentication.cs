namespace chalk.Server.Common.Errors;

public static partial class Errors
{
    public static class Authentication
    {
        public const string InvalidCredentials = "Invalid credentials.";

        public const string UnableToSetRefreshToken = "Unable to set refresh token.";

        public const string RefreshTokenExpired = "Refresh token is expired.";

        public const string RefreshTokenInvalid = "Refresh token is invalid.";
    }
}