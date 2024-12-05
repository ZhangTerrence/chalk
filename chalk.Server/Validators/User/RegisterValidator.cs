using chalk.Server.DTOs.User;
using FluentValidation;

namespace chalk.Server.Validators.User;

public class RegisterValidator : AbstractValidator<RegisterDTO>
{
    public RegisterValidator()
    {
        RuleFor(e => e.FirstName)
            .NotNull()
            .WithMessage("First name is required.")
            .Length(1, 31)
            .WithMessage("First name must be between 1 and 31 characters.");

        RuleFor(e => e.LastName)
            .NotNull()
            .WithMessage("Last name is required.")
            .Length(1, 31)
            .WithMessage("Last name must be between 1 and 31 characters.");

        RuleFor(e => e.DisplayName)
            .NotNull()
            .WithMessage("Display name is required.")
            .Length(3, 31)
            .WithMessage("Display name must be between 3 and 31 characters.");

        RuleFor(e => e.Email)
            .NotNull()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Email is invalid.");

        RuleFor(e => e.Password)
            .NotNull()
            .WithMessage("Password is required.")
            .Matches("^(?:(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[-._@+]).*){8,}$")
            .WithMessage(
                "Password must be at least 8 characters with least one number, one lowercase letter, one upper case letter, one special character.");
    }
}