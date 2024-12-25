using chalk.Server.DTOs;
using chalk.Server.Entities;
using chalk.Server.Extensions;
using chalk.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using NSubstitute;

namespace chalk.UnitTests.Tests.Services;

public class AuthServiceUnitTests
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<long>> _roleManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly AuthService _authService;

    public AuthServiceUnitTests()
    {
        _configuration = Substitute.For<IConfiguration>();
        _configuration["Jwt:Key"] = "________________________________";

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
        _roleManager = Substitute.For<RoleManager<IdentityRole<long>>>(
            Substitute.For<IRoleStore<IdentityRole<long>>>(),
            null,
            null,
            null,
            null
        );
        _httpContextAccessor = Substitute.For<IHttpContextAccessor>();

        _authService = new AuthService(_configuration, _userManager, _roleManager, _httpContextAccessor);
    }

    [Fact]
    public async Task RegisterUserAsync_UserExists_ShouldFail()
    {
        // Arrange
        var registerDTO = new RegisterDTO("Test", "User", "Test User", "tuser1@gmail.com", "@Password123");

        _userManager.FindByEmailAsync(Arg.Any<string>()).Returns(registerDTO.ToUser());

        // Act
        var act = async () => { await _authService.RegisterUserAsync(registerDTO); };

        // Assert
        await act.Should().ThrowAsync<BadHttpRequestException>()
            .Where(e => e.StatusCode == StatusCodes.Status409Conflict)
            .WithMessage("User already exists.");
    }

    [Fact]
    public async Task AuthenticateUserAsync_UserDoesNotExists_ShouldFail()
    {
        // Arrange
        var loginDTO = new LoginDTO("tuser1@gmail.com", "@Password123");

        _userManager.FindByEmailAsync(Arg.Any<string>()).Returns((User?)null);

        // Act
        var act = async () => { await _authService.AuthenticateUserAsync(loginDTO); };

        // Assert
        await act.Should().ThrowAsync<BadHttpRequestException>()
            .Where(e => e.StatusCode == StatusCodes.Status404NotFound)
            .WithMessage("User not found.");
    }
}