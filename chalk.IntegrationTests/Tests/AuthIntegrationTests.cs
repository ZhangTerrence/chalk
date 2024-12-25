using System.Net;
using System.Net.Http.Json;
using chalk.Server.DTOs;
using chalk.Server.DTOs.Responses;
using chalk.Server.Extensions;
using Microsoft.EntityFrameworkCore;

namespace chalk.IntegrationTests.Tests;

public class AuthIntegrationTests(IntegrationTestFactory factory, ITestOutputHelper logger)
    : IntegrationTest(factory, logger)
{
    private readonly IntegrationTestFactory _factory = factory;

    [Fact]
    public async Task Register_ShouldOk()
    {
        // Arrange
        var registerDTO = new RegisterDTO("Test", "User", "Test User 1", "tuser1@gmail.com", "@Password123");

        // Act
        var response = await HttpClient.PostAsJsonAsync("/api/auth/register", registerDTO);

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

        var userRole = await _factory.DatabaseContext.UserRoles
            .Where(e => user != null && e.UserId == user.Id && role != null && e.RoleId == role.Id)
            .FirstOrDefaultAsync();
        userRole.Should().NotBeNull();

        var cookies = GetResponseCookies(response).Where(e => !e.Expired).ToList();
        cookies.Should().HaveCount(2);
    }

    [Fact]
    public async Task Login_ShouldOk()
    {
        // Arrange
        var registerDTO = new RegisterDTO("Test", "User", "Test User 2", "tuser2@gmail.com", "@Password123");
        await HttpClient.PostAsJsonAsync("/api/auth/register", registerDTO);

        var loginDTO = new LoginDTO("tuser2@gmail.com", "@Password123");

        // Act
        var response = await HttpClient.PostAsJsonAsync("/api/auth/login", loginDTO);

        // Assert
        response.Should().BeSuccessful();

        var body = await response.Content.ReadFromJsonAsync<ApiResponseDTO<UserResponseDTO>>();
        body.Should().NotBeNull();
        body?.Errors.Should().BeNull();
        body?.Data.Should().NotBeNull();

        var user = await _factory.DatabaseContext.Users.FindAsync(body?.Data?.Id);
        user.Should().NotBeNull();
        user?.ToUserResponseDTO().Should().BeEquivalentTo(body?.Data);

        var cookies = GetResponseCookies(response).Where(e => !e.Expired).ToList();
        cookies.Should().HaveCount(2);
    }

    [Fact]
    public async Task Login_InvalidCredentials_ShouldUnauthorized()
    {
        // Arrange
        var registerDTO = new RegisterDTO("Test", "User", "Test User 2", "tuser2@gmail.com", "@Password123");
        await HttpClient.PostAsJsonAsync("/api/auth/register", registerDTO);

        var loginDTO = new LoginDTO("tuser2@gmail.com", "@InvalidPassword123");

        // Act
        var response = await HttpClient.PostAsJsonAsync("/api/auth/login", loginDTO);

        // Assert
        response.Should().HaveStatusCode(HttpStatusCode.Unauthorized);

        var cookies = GetResponseCookies(response).Where(e => !e.Expired).ToList();
        cookies.Should().HaveCount(2);
    }

    [Fact]
    public async Task Logout_ShouldOk()
    {
        // Arrange
        new CookiesBuilder()
            .AddAccessToken()
            .AddRefreshToken()
            .Build(HttpClient);

        // Act
        var response = await HttpClient.DeleteAsync("/api/auth/logout");

        // Assert
        response.Should().BeSuccessful();

        var body = await response.Content.ReadAsStringAsync();
        body.Should().BeEmpty();

        var cookies = GetResponseCookies(response).Where(e => !e.Expired).ToList();
        cookies.Should().HaveCount(0);
    }

    [Fact]
    public async Task RefreshTokens_ShouldOk()
    {
        // Arrange
        var registerDTO = new RegisterDTO("Test", "User", "Test User 3", "tuser3@gmail.com", "@Password123");
        var registerResponse = await HttpClient.PostAsJsonAsync("/api/auth/register", registerDTO);
        var registerBody = await registerResponse.Content.ReadFromJsonAsync<ApiResponseDTO<UserResponseDTO>>();

        var cookies = GetResponseCookies(registerResponse).Where(e => !e.Expired).ToList();
        CookiesBuilder.Build(cookies[0].Value, cookies[1].Value, HttpClient);
        Thread.Sleep(1000);

        // Act
        var response = await HttpClient.PatchAsync("/api/auth/refresh", null);

        // Assert
        response.Should().BeSuccessful();

        var body = await response.Content.ReadAsStringAsync();
        body.Should().BeEmpty();

        var updatedCookies = GetResponseCookies(response).ToList();
        updatedCookies.Should().HaveCount(2);

        updatedCookies[0].Should().NotBeEquivalentTo(cookies[0]);
        updatedCookies[1].Should().NotBeEquivalentTo(cookies[1]);

        var user = await _factory.DatabaseContext.Users.FindAsync(registerBody?.Data?.Id);
        user.Should().NotBeNull();
        user?.RefreshToken.Should().NotBeEquivalentTo(cookies[1].Value);
    }
}