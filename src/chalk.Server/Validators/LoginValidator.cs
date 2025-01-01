using chalk.Server.DTOs.Requests;
using FluentValidation;

namespace chalk.Server.Validators;

public class LoginValidator : AbstractValidator<LoginRequest>
{
    public LoginValidator()
    {
        RuleFor(e => e.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Email is invalid.");
        RuleFor(e => e.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .Matches("^(?:(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[-._@+]).*).{8,}$")
            .WithMessage(
                "Password must have at least 8 characters with least one number, one lowercase letter, one upper case letter, one special character.");
    }
}