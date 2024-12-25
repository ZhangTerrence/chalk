using chalk.Server.DTOs.Requests;
using chalk.Server.Validators;
using FluentValidation.TestHelper;

namespace chalk.UnitTests.Requests;

public class LoginRequestTests
{
    private readonly LoginValidator _validator = new();

    [Fact]
    public async Task Invalid_Email()
    {
        // Arrange 
        var loginDTO = new LoginRequest("invalid", "@Password123");

        // Act
        var result = await _validator.TestValidateAsync(loginDTO);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Fact]
    public async Task Invalid_Password()
    {
        // Arrange 
        var loginDTO = new LoginRequest("tuser1@gmail.com", "Password123");

        // Act
        var result = await _validator.TestValidateAsync(loginDTO);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }

    [Fact]
    public async Task Valid()
    {
        // Arrange 
        var loginDTO = new LoginRequest("tuser1@gmail.com", "@Password123");

        // Act
        var result = await _validator.TestValidateAsync(loginDTO);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}