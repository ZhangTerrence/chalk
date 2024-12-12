using chalk.Server.DTOs;
using FluentValidation;

namespace chalk.Server.Validators;

public class RegisterValidator : AbstractValidator<RegisterDTO>
{
    public RegisterValidator()
    {
        RuleFor(e => e.FirstName)
            .NotNull()
            .WithMessage("FirstName property is required.")
            .Length(1, 31)
            .WithMessage("FirstName property must have between 1 and 31 characters.");
        RuleFor(e => e.LastName)
            .NotNull()
            .WithMessage("LastName property is required.")
            .Length(1, 31)
            .WithMessage("LastName property must have between 1 and 31 characters.");
        RuleFor(e => e.DisplayName)
            .NotNull()
            .WithMessage("DisplayName property is required.")
            .Length(3, 31)
            .WithMessage("DisplayName property must have between 3 and 31 characters.");
        RuleFor(e => e.Email)
            .NotEmpty()
            .WithMessage("Email property is required.")
            .EmailAddress()
            .WithMessage("Email property is invalid.");
        RuleFor(e => e.Password)
            .NotNull()
            .WithMessage("Password property is required.")
            .Matches("^(?:(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[-._@+]).*){8,}$")
            .WithMessage(
                "Password property must have at least 8 characters with least one number, one lowercase letter, one upper case letter, one special character.");
    }
}