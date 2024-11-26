using chalk.Server.DTOs.User;
using FluentValidation;

namespace chalk.Server.Validators.User;

public class LoginValidator : AbstractValidator<LoginDTO>
{
    public LoginValidator()
    {
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