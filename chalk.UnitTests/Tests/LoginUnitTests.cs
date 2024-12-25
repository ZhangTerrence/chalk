using chalk.Server.DTOs;
using chalk.Server.Validators;
using FluentValidation.TestHelper;

namespace chalk.UnitTests.Tests;

public class LoginUnitTests
{
    private readonly LoginValidator _validator = new();

    [Fact]
    public async Task Invalid_Email()
    {
        // Arrange 
        var loginDTO = new LoginDTO("invalid", "@Password123");

        // Act
        var result = await _validator.TestValidateAsync(loginDTO);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Fact]
    public async Task Invalid_Password()
    {
        // Arrange 
        var loginDTO = new LoginDTO("tuser1@gmail.com", "Password123");

        // Act
        var result = await _validator.TestValidateAsync(loginDTO);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }

    [Fact]
    public async Task Valid()
    {
        // Arrange 
        var loginDTO = new LoginDTO("tuser1@gmail.com", "@Password123");

        // Act
        var result = await _validator.TestValidateAsync(loginDTO);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}