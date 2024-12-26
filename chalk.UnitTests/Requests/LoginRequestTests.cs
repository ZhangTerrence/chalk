using chalk.Server.Common.Errors;
using chalk.Server.DTOs.Requests;
using chalk.Server.Validators;
using FluentValidation.TestHelper;

namespace chalk.UnitTests.Requests;

public class LoginRequestTests
{
    private readonly LoginValidator _validator = new();

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task Validate_EmailIsNullEmptyOrWhitespace_ShouldHaveError(string? email)
    {
        // Arrange 
        var loginRequest = new LoginRequest(email, "@Password123");

        // Act
        var result = await _validator.TestValidateAsync(loginRequest);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email)
            .WithErrorMessage(Errors.Validation.IsRequired("Email"));
    }

    [Theory]
    [InlineData("tuser")]
    [InlineData("tuser@")]
    [InlineData("@gmail.com")]
    public async Task Validate_EmailIsInvalid_ShouldHaveError(string? email)
    {
        // Arrange 
        var loginRequest = new LoginRequest(email, "@Password123");

        // Act
        var result = await _validator.TestValidateAsync(loginRequest);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email)
            .WithErrorMessage(Errors.Validation.IsInvalid("Email"));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task Validate_PasswordIsNullEmptyOrWhitespace_ShouldHaveError(string? password)
    {
        // Arrange 
        var loginRequest = new LoginRequest("tuser@gmail.com", password);

        // Act
        var result = await _validator.TestValidateAsync(loginRequest);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password)
            .WithErrorMessage(Errors.Validation.IsRequired("Password"));
    }

    [Theory]
    [InlineData("@Pa123")] // Less than 8 characters
    [InlineData("@Password")] // No number
    [InlineData("@PASSWORD123")] // No lowercase letter
    [InlineData("@password123")] // No uppercase letter
    [InlineData("Password123")] // No special character
    public async Task Validate_PasswordIsInvalid_ShouldHaveError(string? password)
    {
        // Arrange 
        var loginRequest = new LoginRequest("tuser@gmail.com", password);

        // Act
        var result = await _validator.TestValidateAsync(loginRequest);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password)
            .WithErrorMessage(Errors.Validation.IsInvalidPassword);
    }

    [Fact]
    public async Task Validate_ValidRequest_ShouldNotHaveError()
    {
        // Arrange 
        var loginRequest = new LoginRequest("tuser@gmail.com", "@Password123");

        // Act
        var result = await _validator.TestValidateAsync(loginRequest);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}