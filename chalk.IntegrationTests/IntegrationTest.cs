using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace chalk.IntegrationTests;

[CollectionDefinition("IntegrationTests", DisableParallelization = true)]
public class DatabaseCollection : ICollectionFixture<IntegrationTestFactory>;

[Collection("IntegrationTests")]
public class IntegrationTest(IntegrationTestFactory factory, ITestOutputHelper logger)
    : IClassFixture<IntegrationTestFactory>
{
    private static readonly Uri BaseUri = new("http://localhost/api");

    protected ITestOutputHelper Logger { get; } = logger;

    protected HttpClient HttpClient { get; } = factory.CreateClient(new WebApplicationFactoryClientOptions
    {
        HandleCookies = true,
        BaseAddress = BaseUri,
    });

    protected static CookieCollection GetResponseCookies(HttpResponseMessage response)
    {
        var cookieContainer = new CookieContainer();
        if (!response.Headers.TryGetValues("Set-Cookie", out var cookies))
        {
            return new CookieCollection();
        }

        foreach (var cookie in cookies)
        {
            cookieContainer.SetCookies(BaseUri, cookie);
        }

        return cookieContainer.GetAllCookies();
    }

    protected sealed class CookiesBuilder
    {
        private AccessTokenBuilder? _accessTokenBuilder;
        private string? _refreshToken;

        public CookiesBuilder AddAccessToken()
        {
            _accessTokenBuilder = new AccessTokenBuilder();
            return this;
        }

        public CookiesBuilder WithNameIdentifier(long identifier)
        {
            _accessTokenBuilder = _accessTokenBuilder?.WithNameIdentifier(identifier);
            return this;
        }

        public CookiesBuilder WithName(string name)
        {
            _accessTokenBuilder = _accessTokenBuilder?.WithName(name);
            return this;
        }

        public CookiesBuilder WithRole(string role)
        {
            _accessTokenBuilder = _accessTokenBuilder?.WithRole(role);
            return this;
        }

        public CookiesBuilder AddRefreshToken()
        {
            var bytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            _refreshToken = Convert.ToBase64String(bytes);
            return this;
        }

        public static void Build(string accessToken, string refreshToken, HttpClient httpClient)
        {
            var cookieContainer = new CookieContainer();
            cookieContainer.Add(BaseUri, new Cookie("AccessToken", accessToken));
            cookieContainer.Add(BaseUri, new Cookie("RefreshToken", refreshToken));

            foreach (Cookie cookie in cookieContainer.GetCookies(BaseUri))
            {
                httpClient.DefaultRequestHeaders.Add("Cookie", $"{cookie.Name}={cookie.Value}");
            }
        }

        public void Build(HttpClient httpClient)
        {
            var cookieContainer = new CookieContainer();
            cookieContainer.Add(BaseUri, new Cookie("AccessToken", _accessTokenBuilder?.Build()));
            cookieContainer.Add(BaseUri, new Cookie("RefreshToken", _refreshToken));

            foreach (Cookie cookie in cookieContainer.GetCookies(BaseUri))
            {
                httpClient.DefaultRequestHeaders.Add("Cookie", $"{cookie.Name}={cookie.Value}");
            }
        }

        internal sealed class AccessTokenBuilder
        {
            private readonly ClaimsIdentity _claims = new();

            public AccessTokenBuilder WithNameIdentifier(long identifier)
            {
                _claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, identifier.ToString()));
                return this;
            }

            public AccessTokenBuilder WithName(string name)
            {
                _claims.AddClaim(new Claim(ClaimTypes.Name, name));
                return this;
            }

            public AccessTokenBuilder WithRole(string role)
            {
                _claims.AddClaim(new Claim(ClaimTypes.Role, role));
                return this;
            }

            public string Build()
            {
                var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(IntegrationTestFactory.Key));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = _claims,
                    Issuer = IntegrationTestFactory.Issuer,
                    Audience = IntegrationTestFactory.Audience,
                    Expires = DateTime.UtcNow.AddDays(1).ToUniversalTime(),
                    SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
        }
    }
}