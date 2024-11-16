using chalk.Server.DTOs;
using FluentValidation;

namespace chalk.Server.Validators;

public class LoginRequestValidator : AbstractValidator<LoginRequestDTO>
{
    public LoginRequestValidator()
    {
        RuleFor(e => e.Email)
            .NotNull()
            .WithMessage("Email is required.");
        RuleFor(e => e.Email)
            .EmailAddress()
            .WithMessage("Invalid email.");

        RuleFor(e => e.Password)
            .NotNull()
            .WithMessage("Password is required.");
        RuleFor(e => e.Password)
            .Matches("^(?:(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[-._@+]).*){8,}$")
            .WithMessage(
                "Password must have at least 8 characters with least one number, one lowercase letter, one upper case letter, one special character.");
    }
}