using System.Net.Http.Headers;
using System.Net.Http.Json;
using chalk.Server.DTOs;
using chalk.Server.DTOs.User;
using chalk.Server.Extensions;
using Microsoft.EntityFrameworkCore;

namespace chalk.IntegrationTests.Tests;

public class UserIntegrationTests(IntegrationTestFactory factory, ITestOutputHelper logger)
    : IntegrationTest(factory, logger)
{
    private readonly IntegrationTestFactory _factory = factory;

    [Fact]
    public async Task RegisterUser_Ok()
    {
        // Arrange
        var registerUserDTO = new RegisterDTO("Test", "User", "Test User 1", "tuser1@gmail.com", "@Password123");

        // Act
        var response = await HttpClient.PostAsJsonAsync("/api/user/register", registerUserDTO);

        // Assert
        response.Should().BeSuccessful();

        var body = await response.Content.ReadFromJsonAsync<ApiResponseDTO<UserResponseDTO>>();
        body.Should().NotBeNull();
        body?.Errors.Should().BeNull();
        body?.Data.Should().NotBeNull();

        var user = await _factory.DatabaseContext.Users.FindAsync(body?.Data?.Id);
        user.Should().NotBeNull();
        user?.ToUserResponseDTO().Should().BeEquivalentTo(body?.Data);

        var role = await _factory.DatabaseContext.Roles.Where(e => e.Name == "User").FirstOrDefaultAsync();
        role.Should().NotBeNull();

        var userInRole = await _factory.DatabaseContext.UserRoles
            .Where(e => user != null && e.UserId == user.Id && role != null && e.RoleId == role.Id)
            .FirstOrDefaultAsync();
        userInRole.Should().NotBeNull();

        response.Headers.TryGetValues("Set-Cookie", out var cookies);
        cookies.Should().HaveCount(2);
    }

    [Fact]
    public async Task LoginUser_Ok()
    {
        // Arrange
        var registerUserDTO = new RegisterDTO("Test", "User", "Test User 2", "tuser2@gmail.com", "@Password123");
        var loginDTO = new LoginDTO("tuser2@gmail.com", "@Password123");

        // Act
        await HttpClient.PostAsJsonAsync("/api/user/register", registerUserDTO);
        var response = await HttpClient.PostAsJsonAsync("/api/user/login", loginDTO);

        // Assert
        response.Should().BeSuccessful();

        var body = await response.Content.ReadFromJsonAsync<ApiResponseDTO<UserResponseDTO>>();
        body.Should().NotBeNull();
        body?.Errors.Should().BeNull();
        body?.Data.Should().NotBeNull();

        response.Headers.TryGetValues("Set-Cookie", out var cookies);
        cookies.Should().HaveCount(2);
    }

    [Fact]
    public async Task LoginUser_IncorrectPassword_Unauthorized()
    {
        // Arrange
        var registerUserDTO = new RegisterDTO("Test", "User", "Test User 3", "tuser3@gmail.com", "@Password123");
        var loginDTO = new LoginDTO("tuser3@gmail.com", "@Password1234");

        // Act
        await HttpClient.PostAsJsonAsync("/api/user/register", registerUserDTO);
        var response = await HttpClient.PostAsJsonAsync("/api/user/login", loginDTO);

        // Assert
        response.Should().HaveError();

        var body = await response.Content.ReadFromJsonAsync<ApiResponseDTO<UserResponseDTO>>();
        body.Should().NotBeNull();
        body?.Errors.Should().HaveCount(1);
        body?.Data.Should().BeNull();

        response.Headers.TryGetValues("Set-Cookie", out var cookies);
        cookies.Should().BeNull();
    }
}