using chalk.Server.DTOs.Requests;
using chalk.Server.Validators;
using FluentValidation.TestHelper;

namespace chalk.UnitTests.Tests;

public class RegisterUnitTests
{
    private readonly RegisterValidator _validator = new();

    [Fact]
    public async Task Invalid_FirstName()
    {
        // Arrange 
        var registerDTO = new RegisterRequest("", "User", "Test User", "tuser1@gmail.com", "@Password123");

        // Act
        var result = await _validator.TestValidateAsync(registerDTO);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FirstName);
    }

    [Fact]
    public async Task Invalid_LastName()
    {
        // Arrange 
        var registerDTO = new RegisterRequest("Test", "", "Test User", "tuser1@gmail.com", "@Password123");

        // Act
        var result = await _validator.TestValidateAsync(registerDTO);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.LastName);
    }

    [Fact]
    public async Task Invalid_DisplayName()
    {
        // Arrange 
        var registerDTO = new RegisterRequest("Test", "User", "", "tuser1@gmail.com", "@Password123");

        // Act
        var result = await _validator.TestValidateAsync(registerDTO);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.DisplayName);
    }

    [Fact]
    public async Task Invalid_Email()
    {
        // Arrange 
        var registerDTO = new RegisterRequest("Test", "User", "Test User", "invalid", "@Password123");

        // Act
        var result = await _validator.TestValidateAsync(registerDTO);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Fact]
    public async Task Invalid_Password()
    {
        // Arrange 
        var registerDTO = new RegisterRequest("Test", "User", "Test User", "tuser1@gmail.com", "Password123");

        // Act
        var result = await _validator.TestValidateAsync(registerDTO);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }

    [Fact]
    public async Task Valid()
    {
        // Arrange 
        var registerDTO = new RegisterRequest("Test", "User", "Test User", "tuser1@gmail.com", "@Password123");

        // Act
        var result = await _validator.TestValidateAsync(registerDTO);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}