using chalk.Server.Common.Errors;
using chalk.Server.DTOs.Requests;
using FluentValidation;

namespace chalk.Server.Validators;

public class RegisterValidator : AbstractValidator<RegisterRequest>
{
    public RegisterValidator()
    {
        RuleFor(e => e.FirstName)
            .NotEmpty()
            .WithMessage(Errors.Validation.IsRequired("FirstName"))
            .Length(1, 31)
            .WithMessage(Errors.Validation.IsBetween("FirstName", 1, 31));
        RuleFor(e => e.LastName)
            .NotEmpty()
            .WithMessage(Errors.Validation.IsRequired("LastName"))
            .Length(1, 31)
            .WithMessage(Errors.Validation.IsBetween("LastName", 1, 31));
        RuleFor(e => e.DisplayName)
            .NotEmpty()
            .WithMessage(Errors.Validation.IsRequired("DisplayName"))
            .Length(3, 31)
            .WithMessage(Errors.Validation.IsBetween("DisplayName", 3, 31));
        RuleFor(e => e.Email)
            .NotEmpty()
            .WithMessage(Errors.Validation.IsRequired("Email"))
            .EmailAddress()
            .WithMessage(Errors.Validation.IsInvalid("Email"));
        RuleFor(e => e.Password)
            .NotEmpty()
            .WithMessage(Errors.Validation.IsRequired("Password"))
            .Matches("^(?:(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[-._@+]).*).{8,}$")
            .WithMessage(Errors.Validation.IsInvalidPassword);
    }
}