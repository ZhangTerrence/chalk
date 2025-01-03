using System.Security.Claims;
using chalk.Server.Configurations;
using chalk.Server.DTOs.Requests;
using chalk.Server.Entities;
using chalk.Server.Mappings;
using chalk.Server.Services;
using chalk.UnitTests.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace chalk.UnitTests.Services;

public class AuthenticationServiceUnitTests : BaseUnitTests
{
    private readonly UserManager<User> _userManager;

    private readonly AuthenticationService _sut;

    public AuthenticationServiceUnitTests()
    {
        var configuration = Substitute.For<IConfiguration>();
        configuration["Jwt:Key"] = SecurityKey;
        configuration["Jwt:Issuer"] = Issuer;
        configuration["Jwt:Audience"] = Audience;

        _userManager = Substitute.For<UserManager<User>>(
            Substitute.For<IUserStore<User>>(),
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null
        );
        var roleManager = Substitute.For<RoleManager<IdentityRole<long>>>(
            Substitute.For<IRoleStore<IdentityRole<long>>>(),
            null,
            null,
            null,
            null
        );
        var httpContextAccessor = Substitute.For<IHttpContextAccessor>();

        _sut = new AuthenticationService(_userManager, roleManager, configuration, httpContextAccessor);
    }

    [Fact]
    public async Task RegisterUserAsync_UserAlreadyExists_ShouldThrow()
    {
        // Arrange
        var registerDTO = new RegisterRequest("Test", "User", "Test User", "tuser1@gmail.com", "@Password123");

        _userManager.FindByEmailAsync(Arg.Any<string>()).Returns(registerDTO.ToEntity());

        // Act
        var act = async () => { await _sut.RegisterUserAsync(registerDTO); };

        // Assert
        await act.Should().ThrowAsync<ServiceException>()
            .Where(e => e.StatusCode == StatusCodes.Status409Conflict);
    }

    [Fact]
    public async Task LoginUserAsync_UserDoesNotExist_ShouldThrow()
    {
        // Arrange
        var loginDTO = new LoginRequest("tuser1@gmail.com", "@Password123");

        _userManager.FindByEmailAsync(Arg.Any<string>()).ReturnsNull();

        // Act
        var act = async () => { await _sut.LoginUserAsync(loginDTO); };

        // Assert
        await act.Should().ThrowAsync<ServiceException>()
            .Where(e => e.StatusCode == StatusCodes.Status404NotFound);
    }

    [Fact]
    public async Task RefreshTokensAsync_AccessTokenIsNull_ShouldThrow()
    {
        // Arrange

        // Act
        var act = async () => { await _sut.RefreshTokensAsync(null, "test-refresh-token"); };

        // Assert
        await act.Should().ThrowAsync<ServiceException>()
            .Where(e => e.StatusCode == StatusCodes.Status400BadRequest);
    }

    [Fact]
    public async Task RefreshTokensAsync_RefreshTokenIsNull_ShouldThrow()
    {
        // Arrange

        // Act
        var act = async () => { await _sut.RefreshTokensAsync("test-access-token", null); };

        // Assert
        await act.Should().ThrowAsync<ServiceException>()
            .Where(e => e.StatusCode == StatusCodes.Status401Unauthorized);
    }

    [Fact]
    public async Task RefreshTokensAsync_UserNotFound_ShouldThrow()
    {
        // Arrange
        var (accessToken, refreshToken) = new TokenBuilder()
            .AddAccessToken().WithNameIdentifier(1)
            .AddRefreshToken()
            .Build();

        _userManager.FindByIdAsync(Arg.Any<string>()).ReturnsNull();

        // Act
        var act = async () => { await _sut.RefreshTokensAsync(accessToken, refreshToken); };

        // Assert
        await act.Should().ThrowAsync<ServiceException>()
            .Where(e => e.StatusCode == StatusCodes.Status404NotFound);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("test-incorrect-refresh-token")]
    public async Task RefreshTokensAsync_RefreshTokenIsInvalid_ShouldThrow(string refreshToken)
    {
        // Arrange
        var (accessToken, _) = new TokenBuilder()
            .AddAccessToken().WithNameIdentifier(1)
            .AddRefreshToken()
            .Build();

        var user = Substitute.For<User>();
        user.RefreshToken = "test-refresh-token";

        _userManager.FindByIdAsync(Arg.Any<string>()).Returns(user);

        // Act
        var act = async () => { await _sut.RefreshTokensAsync(accessToken, refreshToken); };

        // Assert
        await act.Should().ThrowAsync<ServiceException>()
            .Where(e => e.StatusCode == StatusCodes.Status401Unauthorized);
    }

    [Fact]
    public async Task RefreshTokensAsync_RefreshTokenIsExpired_ShouldThrow()
    {
        // Arrange
        var (accessToken, refreshToken) = new TokenBuilder()
            .AddAccessToken().WithNameIdentifier(1)
            .AddRefreshToken()
            .Build();

        var user = Substitute.For<User>();
        user.RefreshToken = "test-refresh-token";
        user.RefreshTokenExpiryDate = DateTime.UtcNow.AddDays(-1);

        _userManager.FindByIdAsync(Arg.Any<string>()).Returns(user);

        // Act
        var act = async () => { await _sut.RefreshTokensAsync(accessToken, refreshToken); };

        // Assert
        await act.Should().ThrowAsync<ServiceException>()
            .Where(e => e.StatusCode == StatusCodes.Status401Unauthorized);
    }

    [Fact]
    public async Task LogoutAsync_UserNotFound_ShouldThrow()
    {
        // Arrange
        var identity = Substitute.For<ClaimsPrincipal>();
        _userManager.GetUserAsync(Arg.Any<ClaimsPrincipal>()).ReturnsNull();

        // Act
        var act = async () => { await _sut.LogoutUserAsync(identity); };

        // Assert
        await act.Should().ThrowAsync<ServiceException>()
            .Where(e => e.StatusCode == StatusCodes.Status404NotFound);
    }
}