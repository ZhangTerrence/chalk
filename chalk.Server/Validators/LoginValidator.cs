using chalk.Server.Common.Errors;
using chalk.Server.DTOs.Requests;
using FluentValidation;

namespace chalk.Server.Validators;

public class LoginValidator : AbstractValidator<LoginRequest>
{
    public LoginValidator()
    {
        RuleFor(e => e.Email)
            .NotEmpty()
            .WithMessage(Errors.Validation.IsRequired("Email"))
            .EmailAddress()
            .WithMessage(Errors.Validation.IsInvalid("Email"));
        RuleFor(e => e.Password)
            .NotEmpty()
            .WithMessage("Password property is required.")
            .Matches("^(?:(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[-._@+]).*){8,}$")
            .WithMessage(Errors.Validation.IsInvalidPassword);
    }
}