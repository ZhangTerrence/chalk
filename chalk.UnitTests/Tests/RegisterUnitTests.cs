using chalk.Server.DTOs;
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
        var registerDTO = new RegisterDTO("", "User", "Test User", "tuser1@gmail.com", "@Password123");

        // Act
        var result = await _validator.TestValidateAsync(registerDTO);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FirstName);
    }

    [Fact]
    public async Task Invalid_LastName()
    {
        // Arrange 
        var registerDTO = new RegisterDTO("Test", "", "Test User", "tuser1@gmail.com", "@Password123");

        // Act
        var result = await _validator.TestValidateAsync(registerDTO);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.LastName);
    }

    [Fact]
    public async Task Invalid_DisplayName()
    {
        // Arrange 
        var registerDTO = new RegisterDTO("Test", "User", "", "tuser1@gmail.com", "@Password123");

        // Act
        var result = await _validator.TestValidateAsync(registerDTO);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.DisplayName);
    }

    [Fact]
    public async Task Invalid_Email()
    {
        // Arrange 
        var registerDTO = new RegisterDTO("Test", "User", "Test User", "invalid", "@Password123");

        // Act
        var result = await _validator.TestValidateAsync(registerDTO);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Fact]
    public async Task Invalid_Password()
    {
        // Arrange 
        var registerDTO = new RegisterDTO("Test", "User", "Test User", "tuser1@gmail.com", "Password123");

        // Act
        var result = await _validator.TestValidateAsync(registerDTO);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }

    [Fact]
    public async Task Valid()
    {
        // Arrange 
        var registerDTO = new RegisterDTO("Test", "User", "Test User", "tuser1@gmail.com", "@Password123");

        // Act
        var result = await _validator.TestValidateAsync(registerDTO);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}