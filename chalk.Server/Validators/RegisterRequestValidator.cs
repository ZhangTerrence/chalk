using chalk.Server.DTOs;
using FluentValidation;

namespace chalk.Server.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequestDTO>
{
    public RegisterRequestValidator()
    {
        RuleFor(e => e.Email)
            .NotNull()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Email must be a valid email address.");

        RuleFor(e => e.DisplayName)
            .NotNull()
            .WithMessage("Display name is required.")
            .Length(3, 31)
            .WithMessage("Display name must be between 3 and 31 characters.");

        RuleFor(e => e.Password)
            .NotNull()
            .WithMessage("Password is required.")
            .Matches("^(?:(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[-._@+]).*){8,}$")
            .WithMessage(
                "Password must have at least 8 characters with least one number, one lowercase letter, one upper case letter, one special character.");
    }
}