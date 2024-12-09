using System.Net;
using chalk.IntegrationTests.Helpers;
using chalk.Server.DTOs;
using chalk.Server.DTOs.User;
using FluentAssertions;

namespace chalk.IntegrationTests;

[Collection("IntegrationTests")]
public class UserRegisterTests(IntegrationTestFactory<Program> factory, ITestOutputHelper logger)
    : BaseTest(factory, logger), IClassFixture<IntegrationTestFactory<Program>>
{
    [Fact]
    public async Task RegisterUser_ShouldSucceed()
    {
        // Arrange
        var registerUserDTO = new RegisterDTO("John", "Doe", "John Doe", "john@gmail.com", "@Password123");

        // Act
        var (response, data) = await HttpClient.PostAsync<ApiResponseDTO<UserResponseDTO>, RegisterDTO>(
            "/api/user/register", registerUserDTO, Logger);

        // Assert
        response
            .Should()
            .BeSuccessful();
        response
            .Should()
            .NotBeNull();
        data?.Errors
            .Should()
            .BeNull();
        data?.Data
            .Should()
            .NotBeNull();
        data?.Data?.FullName
            .Should()
            .Be("John Doe");
    }

    [Fact]
    public async Task RegisterUser_ShouldFail_UserAlreadyExists()
    {
        // Arrange
        var firstUserDTO = new RegisterDTO("John", "Doe", "John Doe", "john@gmail.com", "@Password123");
        var secondUserDTO = new RegisterDTO("John", "Doe", "John Doe", "john@gmail.com", "@Password123");

        // Act
        await HttpClient.PostAsync<ApiResponseDTO<UserResponseDTO>, RegisterDTO>(
            "/api/user/register", firstUserDTO, Logger);
        var (response, data) = await HttpClient.PostAsync<ApiResponseDTO<UserResponseDTO>, RegisterDTO>(
            "/api/user/register", secondUserDTO, Logger);

        // Assert
        response
            .Should()
            .HaveStatusCode(HttpStatusCode.BadRequest);
        response
            .Should()
            .NotBeNull();
        data?.Errors
            .Should()
            .NotBeNull();
        data?.Data
            .Should()
            .BeNull();
        data?.Errors?.FirstOrDefault()
            .Should()
            .NotBeNull();
        data?.Errors?.FirstOrDefault()
            .Should()
            .Be("User already exists.");
    }
}