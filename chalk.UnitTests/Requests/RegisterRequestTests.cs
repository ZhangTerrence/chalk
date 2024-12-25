using chalk.Server.Common.Errors;
using chalk.Server.DTOs.Requests;
using chalk.Server.Validators;
using FluentValidation.TestHelper;

namespace chalk.UnitTests.Requests;

public class RegisterRequestTests
{
    private const string String32 = "________________________________";

    private readonly RegisterValidator _validator = new();

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task Validate_FirstNameIsNullEmptyOrWhitespace_ShouldHaveError(string? firstName)
    {
        // Arrange 
        var registerRequest = new RegisterRequest(firstName, "User", "Test User", "tuser@gmail.com", "@Password123");

        // Act
        var result = await _validator.TestValidateAsync(registerRequest);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FirstName)
            .WithErrorMessage(Errors.Validation.IsRequired("FirstName"));
    }

    [Fact]
    public async Task Validate_FirstNameWrongLength_ShouldHaveError()
    {
        // Arrange 
        var registerRequest = new RegisterRequest(String32, "User", "Test User", "tuser@gmail.com", "@Password123");

        // Act
        var result = await _validator.TestValidateAsync(registerRequest);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FirstName)
            .WithErrorMessage(Errors.Validation.IsBetween("FirstName", 1, 31));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task Validate_LastNameIsNullEmptyOrWhitespace_ShouldHaveError(string? lastName)
    {
        // Arrange 
        var registerRequest = new RegisterRequest("Test", lastName, "Test User", "tuser@gmail.com", "@Password123");

        // Act
        var result = await _validator.TestValidateAsync(registerRequest);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.LastName)
            .WithErrorMessage(Errors.Validation.IsRequired("LastName"));
    }

    [Fact]
    public async Task Validate_LastNameWrongLength_ShouldHaveError()
    {
        // Arrange 
        var registerRequest = new RegisterRequest("Test", String32, "Test User", "tuser@gmail.com", "@Password123");

        // Act
        var result = await _validator.TestValidateAsync(registerRequest);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.LastName)
            .WithErrorMessage(Errors.Validation.IsBetween("LastName", 1, 31));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task Validate_DisplayNameIsNullEmptyOrWhitespace_ShouldHaveError(string? displayName)
    {
        // Arrange 
        var registerRequest = new RegisterRequest("Test", "User", displayName, "tuser@gmail.com", "@Password123");

        // Act
        var result = await _validator.TestValidateAsync(registerRequest);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.DisplayName)
            .WithErrorMessage(Errors.Validation.IsRequired("DisplayName"));
    }

    [Theory]
    [InlineData("_")]
    [InlineData("__")]
    [InlineData(String32)]
    public async Task Validate_DisplayNameWrongLength_ShouldHaveError(string? displayName)
    {
        // Arrange 
        var registerRequest = new RegisterRequest("Test", "User", displayName, "tuser@gmail.com", "@Password123");

        // Act
        var result = await _validator.TestValidateAsync(registerRequest);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.DisplayName)
            .WithErrorMessage(Errors.Validation.IsBetween("DisplayName", 3, 31));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task Validate_EmailIsNullEmptyOrWhitespace_ShouldHaveError(string? email)
    {
        // Arrange 
        var registerRequest = new RegisterRequest("Test", "User", "Test User", email, "@Password123");

        // Act
        var result = await _validator.TestValidateAsync(registerRequest);

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
        var registerRequest = new RegisterRequest("Test", "User", "Test User", email, "@Password123");

        // Act
        var result = await _validator.TestValidateAsync(registerRequest);

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
        var registerRequest = new RegisterRequest("Test", "User", "Test User", "tuser@gmail.com", password);

        // Act
        var result = await _validator.TestValidateAsync(registerRequest);

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
        var registerRequest = new RegisterRequest("Test", "User", "Test User", "tuser@gmail.com", password);

        // Act
        var result = await _validator.TestValidateAsync(registerRequest);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password)
            .WithErrorMessage(Errors.Validation.IsInvalidPassword);
    }
}