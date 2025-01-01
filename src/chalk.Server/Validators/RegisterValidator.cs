using chalk.Server.DTOs.Requests;
using FluentValidation;

namespace chalk.Server.Validators;

public class RegisterValidator : AbstractValidator<RegisterRequest>
{
    public RegisterValidator()
    {
        RuleFor(e => e.FirstName)
            .NotEmpty()
            .WithMessage("First name is required.")
            .Length(1, 31)
            .WithMessage("First name must have between 1 and 31 characters.");
        RuleFor(e => e.LastName)
            .NotEmpty()
            .WithMessage("Last name is required.")
            .Length(1, 31)
            .WithMessage("Last name must have between 1 and 31 characters.");
        RuleFor(e => e.DisplayName)
            .NotEmpty()
            .WithMessage("Display name is required.")
            .Length(3, 31)
            .WithMessage("Display name must have between 3 and 31 characters.");
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