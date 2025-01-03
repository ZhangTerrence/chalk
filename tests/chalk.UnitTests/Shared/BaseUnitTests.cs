using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using chalk.Server.Utilities;
using Microsoft.IdentityModel.Tokens;

namespace chalk.UnitTests.Shared;

public abstract class BaseUnitTests
{
    protected const string Issuer = "test-issuer";
    protected const string Audience = "test-audience";
    protected const string SecurityKey = "________________________________";

    protected sealed class TokenBuilder
    {
        private AccessTokenBuilder? _accessTokenBuilder;
        private string? _refreshToken;

        public TokenBuilder AddAccessToken()
        {
            _accessTokenBuilder = new AccessTokenBuilder();
            return this;
        }

        public TokenBuilder WithNameIdentifier(long identifier)
        {
            _accessTokenBuilder = _accessTokenBuilder?.WithNameIdentifier(identifier);
            return this;
        }

        public TokenBuilder WithName(string name)
        {
            _accessTokenBuilder = _accessTokenBuilder?.WithName(name);
            return this;
        }

        public TokenBuilder WithRole(string role)
        {
            _accessTokenBuilder = _accessTokenBuilder?.WithRole(role);
            return this;
        }

        public TokenBuilder AddRefreshToken()
        {
            _refreshToken = JwtUtilities.CreateRefreshToken();
            return this;
        }

        public (string?, string?) Build()
        {
            return (_accessTokenBuilder?.Build(), _refreshToken);
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
                var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecurityKey));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = _claims,
                    Issuer = Issuer,
                    Audience = Audience,
                    Expires = DateTime.UtcNow.AddMinutes(15),
                    NotBefore = DateTime.UtcNow,
                    SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
        }
    }
}