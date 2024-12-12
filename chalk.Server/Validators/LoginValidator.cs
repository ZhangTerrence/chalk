using chalk.Server.DTOs;
using FluentValidation;

namespace chalk.Server.Validators;

public class LoginValidator : AbstractValidator<LoginDTO>
{
    public LoginValidator()
    {
        RuleFor(e => e.Email)
            .NotEmpty()
            .WithMessage("Email property is required.")
            .EmailAddress()
            .WithMessage("Email property is invalid.");
        RuleFor(e => e.Password)
            .NotEmpty()
            .WithMessage("Password property is required.")
            .Matches("^(?:(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[-._@+]).*){8,}$")
            .WithMessage(
                "Password property must have at least 8 characters with least one number, one lowercase letter, one upper case letter, one special character.");
    }
}