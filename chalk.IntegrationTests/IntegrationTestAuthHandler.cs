using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace chalk.IntegrationTests;

public class IntegrationTestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    [Obsolete("Obsolete")]
    public IntegrationTestAuthHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder, ISystemClock clock
    ) : base(options, logger, encoder, clock)
    {
    }

    public IntegrationTestAuthHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder
    ) : base(options, logger, encoder)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, "1"),
            new Claim(ClaimTypes.NameIdentifier, "Test User 1"),
            new Claim(ClaimTypes.Role, "User"),
        };
        var identity = new ClaimsIdentity(claims, "IntegrationTests");
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, "IntegrationTests");
        var result = AuthenticateResult.Success(ticket);

        return Task.FromResult(result);
    }
}