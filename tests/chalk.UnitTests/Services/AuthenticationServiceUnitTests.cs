using System.Security.Claims;
using chalk.Server.Common.Errors;
using chalk.Server.DTOs.Requests;
using chalk.Server.Entities;
using chalk.Server.Mappings;
using chalk.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace chalk.UnitTests.Services;

public class AuthenticationServiceUnitTests
{
    private readonly UserManager<User> _userManager;

    private readonly AuthenticationService _sut;

    public AuthenticationServiceUnitTests()
    {
        var configuration = Substitute.For<IConfiguration>();
        configuration["Jwt:Key"] = "________________________________";

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

        _sut = new AuthenticationService(configuration, _userManager, roleManager, httpContextAccessor);
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
        await act.Should().ThrowAsync<BadHttpRequestException>()
            .Where(e => e.StatusCode == StatusCodes.Status409Conflict)
            .WithMessage(Errors.AlreadyExists("User"));
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
        await act.Should().ThrowAsync<BadHttpRequestException>()
            .Where(e => e.StatusCode == StatusCodes.Status404NotFound)
            .WithMessage(Errors.NotFound("User"));
    }

    [Fact]
    public async Task RefreshTokensAsync_IdentityIsNull_ShouldThrow()
    {
        // Arrange
        var identity = Substitute.For<ClaimsPrincipal>();
        identity.FindFirstValue(ClaimTypes.NameIdentifier).ReturnsNull();

        // Act
        var act = async () => { await _sut.RefreshTokensAsync(identity, "test-refresh-token"); };

        // Assert
        await act.Should().ThrowAsync<BadHttpRequestException>()
            .Where(e => e.StatusCode == StatusCodes.Status401Unauthorized)
            .WithMessage(Errors.Unauthorized);
    }

    [Fact]
    public async Task RefreshTokensAsync_RefreshTokenIsNull_ShouldThrow()
    {
        // Arrange
        var identity = Substitute.For<ClaimsPrincipal>();
        identity.FindFirst(Arg.Any<string>()).Returns(new Claim(ClaimTypes.NameIdentifier, "test-user-id"));

        // Act
        var act = async () => { await _sut.RefreshTokensAsync(identity, null); };

        // Assert
        await act.Should().ThrowAsync<BadHttpRequestException>()
            .Where(e => e.StatusCode == StatusCodes.Status401Unauthorized)
            .WithMessage(Errors.Unauthorized);
    }

    [Fact]
    public async Task RefreshTokensAsync_UserDoesNotExist_ShouldThrow()
    {
        // Arrange
        var identity = Substitute.For<ClaimsPrincipal>();
        identity.FindFirst(Arg.Any<string>()).Returns(new Claim(ClaimTypes.NameIdentifier, "test-user-id"));

        _userManager.FindByIdAsync(Arg.Any<string>()).ReturnsNull();

        // Act
        var act = async () => { await _sut.RefreshTokensAsync(identity, "test-refresh-token"); };

        // Assert
        await act.Should().ThrowAsync<BadHttpRequestException>()
            .Where(e => e.StatusCode == StatusCodes.Status404NotFound)
            .WithMessage(Errors.NotFound("User"));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("test-incorrect-refresh-token")]
    public async Task RefreshTokensAsync_RefreshTokenInvalid_ShouldThrow(string refreshToken)
    {
        // Arrange
        var identity = Substitute.For<ClaimsPrincipal>();
        identity.FindFirst(Arg.Any<string>()).Returns(new Claim(ClaimTypes.NameIdentifier, "test-user-id"));

        var user = Substitute.For<User>();
        user.RefreshToken = refreshToken;

        _userManager.FindByIdAsync(Arg.Any<string>()).Returns(user);

        // Act
        var act = async () => { await _sut.RefreshTokensAsync(identity, "test-refresh-token"); };

        // Assert
        await act.Should().ThrowAsync<BadHttpRequestException>()
            .Where(e => e.StatusCode == StatusCodes.Status401Unauthorized)
            .WithMessage(Errors.Authentication.RefreshTokenInvalid);
    }

    [Fact]
    public async Task RefreshTokensAsync_RefreshTokenExpired_ShouldThrow()
    {
        // Arrange
        var identity = Substitute.For<ClaimsPrincipal>();
        identity.FindFirst(Arg.Any<string>()).Returns(new Claim(ClaimTypes.NameIdentifier, "test-user-id"));

        var user = Substitute.For<User>();
        user.RefreshToken = "test-refresh-token";
        user.RefreshTokenExpiryDate = DateTime.UtcNow.AddDays(-1);

        _userManager.FindByIdAsync(Arg.Any<string>()).Returns(user);

        // Act
        var act = async () => { await _sut.RefreshTokensAsync(identity, "test-refresh-token"); };

        // Assert
        await act.Should().ThrowAsync<BadHttpRequestException>()
            .Where(e => e.StatusCode == StatusCodes.Status401Unauthorized)
            .WithMessage(Errors.Authentication.RefreshTokenExpired);
    }
}